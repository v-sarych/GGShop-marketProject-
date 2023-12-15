using Integrations.Cdek.Entities.RegisterOrderEntities;
using Integrations.Cdek.Interfaces;
using Microsoft.EntityFrameworkCore;
using ShopDb;
using ShopDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek
{
    public class DeliveryDataFormatter : IDeliveryDataFormatter
    {
        private readonly IShopDbContext _dbContext;
        private readonly CdekIntegrationConfiguration _cdekConfiguration;
        public DeliveryDataFormatter(IShopDbContext dbContext, CdekIntegrationConfiguration cdekIntegrationConfiguration)
            => (_dbContext, _cdekConfiguration) = (dbContext, cdekIntegrationConfiguration);
        public async Task<RegisterOrder> CreateRegisterOrderObject(Guid id)
        {
            RegisterOrder result = new();
            Order order = _dbContext.Orders.AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.AvailabilityOfProduct)
                    .ThenInclude(a => a.PackageSize)
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
                .First(o => o.Id == id);

            result.Number = order.Id;

            result.Recipient = new Recipient()
            {
                Name = order.User.Name,
                Phones = new Phone[1]{ new Phone(){ Number =  order.User.PhoneNumber } }
            };

            result.Tariff_code = _cdekConfiguration.TariffCode;

            _setDeliveryAddresses(result, order);

            _setPackages(result, order.OrderItems);

            return result;
        }

        private void _setPackages(RegisterOrder order, ICollection<OrderItem> items)
        {
            order.Packages = new Package[items.Select(i => i.Count).Sum()];

            int i = 0;
            foreach (var orderItem in items)
            {
                for (int j = 0; j < orderItem.Count; j++)
                {
                    order.Packages[i] = new Package();

                    order.Packages[i].Number = Convert.ToString(i);
                    order.Packages[i].Height = orderItem.AvailabilityOfProduct.PackageSize.Height;
                    order.Packages[i].Length = orderItem.AvailabilityOfProduct.PackageSize.Length;
                    order.Packages[i].Width = orderItem.AvailabilityOfProduct.PackageSize.Width;
                    order.Packages[i].Weight = orderItem.AvailabilityOfProduct.Weight;

                    order.Packages[i].Items = new Item[1];
                    order.Packages[i].Items[0] = new Item()
                    {
                        Payment = new(),
                        Weight = orderItem.AvailabilityOfProduct.Weight,
                        Ware_key = orderItem.Sku,
                        Name = orderItem.Product.Name,
                        Amount = 1,
                        Cost = orderItem.Cost / orderItem.Count
                    };

                    i++;
                }
            }
        }

        private void _setDeliveryAddresses(RegisterOrder registerOrder, Order dbOrder)
        {
            
        }
    }
}
