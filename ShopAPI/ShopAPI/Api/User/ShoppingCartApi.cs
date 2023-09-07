using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Model.Entities.DTO.ShoppingCart;
using ShopApi.Model.Interfaces.Repository;

using ClaimTypes = IdentityServer.Model.Entities.Identity.ClaimTypes;


namespace ShopApi.Api.User
{
    [Route("api/User/ShoppingCart")]
    [Authorize]
    [ApiController]
    public class ShoppingCartApi : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartApi(IShoppingCartRepository shoppingCartRepository)
            => _shoppingCartRepository = shoppingCartRepository;

        [HttpPost("AddItem")]
        public async Task<long> AddItem(AddShoppingCartItemDTO item)
            => await _shoppingCartRepository.AddItem(item,
                Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));

        [HttpPut("UpdateItemCount")]
        public async Task UpdateItemCount(UpdateShoppingCartItemDTO itemWithChanges)
            => await _shoppingCartRepository.UpdateItemCount(itemWithChanges,
                Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));

        [HttpDelete("Remove")]
        public async Task Remove(long itemId)
            => await _shoppingCartRepository.RemoveItem(itemId,
                Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));
    }
}
