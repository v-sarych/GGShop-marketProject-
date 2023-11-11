using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApiCore.Entities.DTO.ShoppingCart;
using ShopApiCore.Interfaces.Repository;
using ClaimTypes = IdentityServer.Model.Entities.Identity.ClaimTypes;


namespace ShopApiServer.Api.User
{
    [Route("api/User/ShoppingCart")]
    [Authorize]
    [ApiController]
    public class ShoppingCartApi : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartApi(IShoppingCartRepository shoppingCartRepository)
            => _shoppingCartRepository = shoppingCartRepository;

        /// <response code="200">Sucess</response>
        /// <response code="400">AlreadyExist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("AddItem")]
        public async Task AddItem(AddShoppingCartItemDTO item)
            => await _shoppingCartRepository.AddItem(item,
                Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));

        [HttpPut("UpdateItemCount")]
        public async Task UpdateItemCount(UpdateShoppingCartItemDTO itemWithChanges)
            => await _shoppingCartRepository.UpdateItemCount(itemWithChanges,
                Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));

        [HttpDelete("Remove")]
        public async Task Remove(string sku)
            => await _shoppingCartRepository.RemoveItem(sku,
                Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));
    }
}
