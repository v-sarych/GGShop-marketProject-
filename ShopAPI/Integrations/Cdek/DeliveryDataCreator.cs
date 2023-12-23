using Integrations.Cdek.Entities.DeliveryCalculationData;
using Integrations.Cdek.Entities.RegisterOrderEntities;
using Integrations.Cdek.Interfaces;
using Microsoft.EntityFrameworkCore;
using ShopDb;
using ShopDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Integrations.Cdek
{
    public class DeliveryDataCreator : IDeliveryDataCreator
    {
        private readonly IShopDbContext _dbContext;
        private readonly CdekIntegrationConfiguration _cdekConfiguration;
        public DeliveryDataCreator(IShopDbContext dbContext, CdekIntegrationConfiguration cdekIntegrationConfiguration)
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

            DeliveryUserData deliveryUserData = _getDeliveryUserData(order.DeliveryInfo);
            result.Tariff_code = (int)deliveryUserData.Tariff_code;
            result.To_location = deliveryUserData.To_location;
            result.From_location = deliveryUserData.From_location;

            result.Packages = _createPackages(order.OrderItems);

            return result;
        }

        public async Task<DeliveryWidgetCalculationDataDTO> GetDataForWidget(Guid id)
        {
            DeliveryWidgetCalculationDataDTO result = new();
            Order order = _dbContext.Orders.AsNoTracking()
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.AvailabilityOfProduct)
                    .ThenInclude(a => a.PackageSize)
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
                .First(o => o.Id == id);

            result.Packages = _createPackages(order.OrderItems);

            DeliveryUserData deliveryUserData = _getDeliveryUserData(order.DeliveryInfo);
            result.To_location = deliveryUserData.To_location;
            result.From_location = deliveryUserData.From_location;

            return result;
        }

        private Package[] _createPackages(ICollection<OrderItem> items)
        {
            Package[] packages = new Package[items.Select(i => i.Count).Sum()];

            int i = 0;
            foreach (var orderItem in items)
            {
                for (int j = 0; j < orderItem.Count; j++)
                {
                    packages[i] = new Package();

                    packages[i].Number = Convert.ToString(i);
                    packages[i].Height = orderItem.AvailabilityOfProduct.PackageSize.Height;
                    packages[i].Length = orderItem.AvailabilityOfProduct.PackageSize.Length;
                    packages[i].Width = orderItem.AvailabilityOfProduct.PackageSize.Width;
                    packages[i].Weight = orderItem.AvailabilityOfProduct.Weight;

                    packages[i].Items = new Item[1];
                    packages[i].Items[0] = new Item()
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

            return packages;
        }

        private DeliveryUserData _getDeliveryUserData(string deliveryData)
            => JsonSerializer.Deserialize<DeliveryUserData>(deliveryData);
    }
}
