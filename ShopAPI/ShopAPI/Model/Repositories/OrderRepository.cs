using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopApi.Model.Entities.DTO.Order;
using ShopApi.Model.Entities.DTO.SearchResults;
using ShopApi.Model.Exceptions;
using ShopApi.Model.Interfaces.Repository;
using ShopDb;
using ShopDb.Entities;
using System.Linq;

namespace ShopApi.Model.Repositories
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
            creatingOrder.DateOfCreation = DateTime.Now;

            creatingOrder.Id = Guid.NewGuid();
            foreach(var item in creatingOrder.OrderItems)
                item.OrderId = creatingOrder.Id;

            IQueryable<AvailabilityOfProduct> aviabilitiesQuery = _dBContext.AvailabilityOfProducts;
            List<AvailabilityOfProduct> availabilities = await aviabilitiesQuery
                .Where(a => createSettings.OrderItems.Select(i => i.ProductId).Any(i => i == a.ProductId))
                .Where(a => createSettings.OrderItems.Select(i => i.Size).Any(i => i == a.Size))
                .ToListAsync();

            foreach (var orderItem in creatingOrder.OrderItems)
            {
                var availability = availabilities.FirstOrDefault(x => x.ProductId == orderItem.ProductId && x.Size == orderItem.Size);
                if (availability == null)
                    throw new NotInStockException();

                if (orderItem.Count > 0 && availability.Count - orderItem.Count >= 0)
                {
                    availability.Count -= orderItem.Count;
                    orderItem.Cost = (float)availability.Cost * orderItem.Count;
                    creatingOrder.Cost += orderItem.Cost;
                }
                else { throw new NotInStockException(); }
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

            /*Order order = new()
            {
                Id = id,
                Status = newStatus,
            };

            await _dBContext.UpdatePropertiesWithoutNull<Order>(order, _dBContext.Orders);*/
        }
    }
}
