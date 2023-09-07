using IdentityServer.Model.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Model.Entities.DTO.Order;
using ShopApi.Model.Interfaces.Repository;
using ShopDb.Entities;

namespace ShopApi.Api
{
    [Route("api/Order")]
    [ApiController]
    public class OrderApi : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderApi(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        [Authorize(Roles = "Admin")]
        [HttpPost("Search")]
        public async Task<ICollection<GetFullOrderInfoDTO>> SearchOrders(OrderSearchSettingsDTO searchSettings)
            => await _orderRepository.GetOrders(searchSettings);

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
    }
}
