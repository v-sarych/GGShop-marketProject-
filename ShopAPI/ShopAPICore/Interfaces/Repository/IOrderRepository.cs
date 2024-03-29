﻿using ShopApiCore.Entities.DTO.Order;
using ShopApiCore.Entities.DTO.Payments;
using ShopDb.Entities;

namespace ShopApiCore.Interfaces.Repository
{
    public interface IOrderRepository
    {
        Task<ICollection<GetFullOrderInfoDTO>> GetOrders(OrderSearchSettingsDTO searchSettings);
        Task<GetUserOrderDTO> CreateWithDelivery(CreateOrderDTO createSettings, long userId);
        Task<GetUserOrderDTO> Create(CreateOrderDTO createSettings, long userId);
        Task UpdateStatus(Guid id, string newStatus);
        Task<OrderStatusesDTO> GetAvailableStatuses();
    }
}
