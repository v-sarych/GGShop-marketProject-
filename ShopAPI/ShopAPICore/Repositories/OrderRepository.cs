using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopApiCore.Entities.DTO.Order;
using ShopApiCore.Entities.DTO.Payments;
using ShopApiCore.Entities.DTO.SearchResults;
using ShopApiCore.Exceptions;
using ShopApiCore.Interfaces.Repository;
using ShopApiCore.Interfaces.Services;
using ShopDb;
using ShopDb.Entities;
using System.Security.Cryptography;

namespace ShopApiCore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IShopDbContext _dBContext;
        private readonly IMapper _mapper;

        public OrderRepository(IShopDbContext dBContext, IMapper mapper)
            => (_dBContext, _mapper) = (dBContext, mapper);

        public async Task<GetUserOrderDTO> Create(CreateOrderDTO createSettings, long userId)
        {
            if(createSettings.OrderItems.Count < 1)
                throw new Exception();

            Order creatingOrder = _mapper.Map<Order>(createSettings);

            creatingOrder.UserId = userId;
            creatingOrder.WebHookKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            creatingOrder.DateOfCreation = DateTime.UtcNow;

            creatingOrder.Id = Guid.NewGuid();
            foreach(var item in creatingOrder.OrderItems)
                item.OrderId = creatingOrder.Id;

            IQueryable<AvailabilityOfProduct> aviabilitiesQuery = _dBContext.AvailabilityOfProducts;
            List<AvailabilityOfProduct> availabilities = await aviabilitiesQuery
                .Where(a => createSettings.OrderItems.Select(i => i.Sku).Any(i => i == a.Sku))
                .ToListAsync();

            foreach (var orderItem in creatingOrder.OrderItems)
            {
                var availability = availabilities.FirstOrDefault(x => x.Sku == orderItem.Sku);
                if (availability == null || availability.Count - orderItem.Count < 0)
                    throw new NotInStockException();

                orderItem.ProductId = availability.ProductId;

                if (orderItem.Count > 0)
                {
                    availability.Count -= orderItem.Count;
                    orderItem.Cost = (float)availability.Cost * orderItem.Count;
                    creatingOrder.Cost += orderItem.Cost;
                }
            }

            await _dBContext.Orders.AddAsync(creatingOrder);
            await _dBContext.SaveChangesAsync();

            return await _dBContext.Orders.AsNoTracking().ProjectTo<GetUserOrderDTO>(_mapper.ConfigurationProvider).FirstAsync(x => x.Id == creatingOrder.Id);
        }

        public async Task<OrderStatusesDTO> GetAvailableStatuses()
           => new OrderStatusesDTO(); 

        public async Task<ICollection<GetFullOrderInfoDTO>> GetOrders(OrderSearchSettingsDTO searchSettings)
        {
            IQueryable<Order> resultQuery = _dBContext.Orders.AsNoTracking()
                .Include(x => x.User);

            if (searchSettings.ProductId != null)
                resultQuery = resultQuery.Where(o => o.OrderItems.Any(i => i.ProductId == searchSettings.ProductId));

            if(searchSettings.UserId != null)
                resultQuery = resultQuery.Where(o => o.UserId == searchSettings.UserId);

            if (searchSettings.OrderStatus != null)
                resultQuery = resultQuery.Where(o => o.Status == searchSettings.OrderStatus);

            if (searchSettings.FirstRangePoint != null && searchSettings.EndRangePoint != null)
                resultQuery = resultQuery.Skip((int)searchSettings.FirstRangePoint)
                    .Take((int)searchSettings.EndRangePoint - (int)searchSettings.FirstRangePoint);

            return await resultQuery.ProjectTo<GetFullOrderInfoDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task UpdateStatus(Guid id, string newStatus)
        {
            Order order = await _dBContext.Orders.AsTracking().FirstAsync(order => order.Id == id);
            order.Status = newStatus;

            await _dBContext.SaveChangesAsync();
        }
    }
}
