using IdentityServer.Model.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ShopApiCore.Entities.DTO.Order;
using ShopApiCore.Entities.DTO.Payments;
using ShopApiCore.Interfaces.Repository;
using ShopApiCore.Interfaces.Services;
using ShopDb.Entities;

namespace ShopApiServer.Api
{
    [Route("api/Order")]
    [ApiController]
    public class OrderApi : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryService _deliveryService;

        public OrderApi(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        [Authorize(Roles = "Admin")]
        [HttpPost("Search")]
        public async Task<ICollection<GetFullOrderInfoDTO>> SearchOrders(OrderSearchSettingsDTO searchSettings)
            => await _orderRepository.GetOrders(searchSettings);

        [HttpPost("Pay")]
        public async Task<string> Pay(PaymentInfoDTO info)
            => await _orderRepository.CreateAndAuthorizeOrderPayment(info);

        [HttpGet("GetAvailableStatuses")]
        public async Task<OrderStatusesDTO> GetAvailableStatuses() 
            => await _orderRepository.GetAvailableStatuses();

        [Authorize]
        [HttpPost("Create")]
        public async Task<GetUserOrderDTO> Create(CreateOrderDTO createSettings)
            => await _orderRepository.Create(createSettings, Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("UpdateStatus")]
        public async Task UpdateStatus(Guid id, string newStatus)
            => await _orderRepository.UpdateStatus(id, newStatus);

        [Authorize]
        [HttpPost("TransferToDelivery")]
        public async Task TransferToDelivery(Guid orderId)
             => await _deliveryService.TransferToDelivery(orderId);
    }
}
