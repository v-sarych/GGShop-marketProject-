using ShopApi.Model.Entities.DTO.Order;
using ShopDb.Entities;

namespace ShopApi.Model.Interfaces.Repository
{
    public interface IOrderRepository
    {
        Task<ICollection<GetFullOrderInfoDTO>> GetOrders(OrderSearchSettingsDTO searchSettings);
        Task<GetUserOrderDTO> Create(CreateOrderDTO createSettings, long userId);
        Task UpdateStatus(Guid id, string newStatus);
        Task<OrderStatusesDTO> GetAvailableStatuses();
    }
}
