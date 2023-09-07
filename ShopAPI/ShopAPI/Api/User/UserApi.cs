using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Model.Entities.DTO.User;
using IdentityServer.Model.Entities.Identity;
using ShopApi.Model.Entities.DTO.ShoppingCart;
using ShopApi.Model.Interfaces.Repository;

using ClaimTypes = IdentityServer.Model.Entities.Identity.ClaimTypes;

namespace ShopApi.Api.User
{
    [Route("api/User")]
    [Authorize]
    [ApiController]
    public class UserApi : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserApi(IUserRepository userRepository)
            => _userRepository = userRepository;

        [HttpGet("Get")]
        public async Task<GetUserDTO> GetUser()
            => await _userRepository.Get(
                Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));

        [HttpPatch("Update")]
        public async Task Update(UpdateUserInfoDTO user)
            => await _userRepository.UpdateInfo(user,
                Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));
    }
}
