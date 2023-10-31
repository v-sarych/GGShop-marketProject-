using Integrations.YourPayments.Entities;
using Integrations.YourPayments.Interfaces;
using Microsoft.EntityFrameworkCore;
using ShopApiCore.Entities.DTO.Payments;
using ShopDb;
using ShopDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments
{
    internal class PaymentDataCreator : IPaymentDataCreator
    {
        private readonly IShopDbContext _dbContext;
        private readonly PaymentConfiguration _configuration;
        public PaymentDataCreator(PaymentConfiguration paymentConfiguration, IShopDbContext shopDbContext)
            => (_configuration, _dbContext) = (paymentConfiguration, _dbContext);

        public async Task<AuthorizePaymentData> FromDTO(PaymentInfoDTO dto)
        {
            AuthorizePaymentData result = new AuthorizePaymentData();


            Order order = _dbContext.Orders.AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.AvailabilityOfProduct)
                .First(o => o.Id == dto.OrderId);

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
                    CountryCode = "",
                    Email = "",
                    LastName = "",
                    Phone = order.User.PhoneNumber
                };

            foreach (var item in order.OrderItems)
                result.Products.Add(new ProductEntity()
                {
                    Name = item.Product.Name,
                    Sku = item.Sku,
                    Quantity = Convert.ToUInt16(item.Count),
                    UnitPrice = Convert.ToString(item.Cost / item.Count)
                });

            return result;
        }
    }
}
