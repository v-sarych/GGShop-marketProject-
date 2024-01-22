using Integrations.Cdek.Entities.RegisterOrderEntities;
using Integrations.YourPayments.Entities;
using Integrations.YourPayments.Entities.AuthorizePayment;
using Integrations.YourPayments.Interfaces;
using Microsoft.EntityFrameworkCore;
using ShopApiCore.Entities.DTO.Payments;
using ShopDb;
using ShopDb.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Integrations.YourPayments
{
    public class PaymentDataCreator : IPaymentDataCreator
    {
        private readonly IShopDbContext _dbContext;
        private readonly PaymentConfiguration _configuration;
        public PaymentDataCreator(PaymentConfiguration paymentConfiguration, IShopDbContext shopDbContext)
            => (_configuration, _dbContext) = (paymentConfiguration, shopDbContext);

        public async Task<AuthorizePaymentData> GetAuthorizePaymentData(PaymentInfoDTO dto)
        {
            AuthorizePaymentData result = new AuthorizePaymentData();


            Order order = _dbContext.Orders.AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.AvailabilityOfProduct)
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
                .First(o => o.Id == dto.OrderId);

            result.ReturnUrl = _configuration.AuthorizePaymentReturnUrl;
            result.MerchantPaymentReference = order.Id.ToString();

            result.Authorization = new AuthorizeEntity()
            {
                PaymentMethod = dto.PaymentMethod,
                UsePaymentPage = "YES"
            };

            result.Currency = dto.Currency;

            if(dto.paymentClientDTO != null)
                result.Client = new ClientEntity()
                {
                    FirstName = dto.paymentClientDTO.FirstName,
                    CountryCode = dto.paymentClientDTO.CountryCode,
                    Email = dto.paymentClientDTO.Email,
                    LastName = dto.paymentClientDTO.LastName,
                    Phone = order.User.PhoneNumber
                };
            else 
                result.Client = new ClientEntity()
                {
                    FirstName = order.User.Name,
                    CountryCode = "RU",
                    Email = "",
                    LastName = "",
                    Phone = order.User.PhoneNumber
                };

            result.Products = new List<ProductEntity>();
            foreach (var item in order.OrderItems)
                result.Products.Add(new ProductEntity()
                {
                    Name = item.Product.Name,
                    Sku = item.Sku,
                    Quantity = Convert.ToUInt16(item.Count),
                    UnitPrice = Convert.ToString(item.Cost / item.Count)
                });

            if(order.AdditionalFees != 0)
                result.Products.Add(new ProductEntity()
                {
                    Name = "AdditionalFees",
                    Sku = "AdditionalFees",
                    Quantity = 1,
                    UnitPrice = Convert.ToString(order.AdditionalFees)
                });

            return result;
        }

        public async Task<string> GetSignature(SignatureParameters parameters)
        {
            byte[] hashedData = Encoding.UTF8.GetBytes(parameters.Merchant + parameters.Date + parameters.HttpMethod + parameters.BaseUrl + parameters.MD5BodySum);

            byte[] hash;
            using (var hasher = new HMACSHA256(Encoding.UTF8.GetBytes(_configuration.SecretKey)))
                hash = hasher.ComputeHash(hashedData);

            return BitConverter.ToString(hash).ToLower().Replace("-", "");
        }

        public async Task<string> GetMD5(byte[] data)
        {
            byte[] md5Sum;
            using(var md5 = MD5.Create())
                md5Sum = md5.ComputeHash(data);

            return BitConverter.ToString(md5Sum).ToLower().Replace("-", "");
        }
    }
}
