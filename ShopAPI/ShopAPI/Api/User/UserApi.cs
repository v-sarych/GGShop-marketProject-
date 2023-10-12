using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IdentityServer.Model.Entities.Identity;
using ShopApiCore.Entities.DTO.User;
using ShopApiCore.Interfaces.Repository;

using ClaimTypes = IdentityServer.Model.Entities.Identity.ClaimTypes;

namespace ShopApiServer.Api.User
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
