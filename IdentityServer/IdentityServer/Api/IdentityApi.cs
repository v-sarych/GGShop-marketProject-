using IdentityServer.Model.Entities.DTO;
using IdentityServer.Model.Entities.Identity;
using IdentityServer.Model.interfaces;
using IdentityServer.Model.interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ShopDb.Entities;
using System.Security.Claims;
using ClaimTypes = IdentityServer.Model.Entities.Identity.ClaimTypes;

namespace IdentityServer.Controllers
{
    [Route("api/Identity")]
    [ApiController]
    public class IdentityApi : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IAuthenticationService _authenticationService;

        public IdentityApi(IUserRepository userRepository, IAuthenticationService authenticationService, ISessionRepository sessionRepository)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _sessionRepository = sessionRepository;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<string> Register(RegisterUserDTO userDTO)
        {
            if (await _userRepository.TryFindUser(userDTO.PhoneNumber))
            {
                Response.StatusCode = 500;
                return "This user is already exist";
            }

            UserIdentity user = await _userRepository.Create(userDTO);

            return await _authenticationService.Login(user);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<string> Login(LoginUserDTO login)
        {
            UserIdentity user = await _userRepository.FindUserOrDefault(login.PhoneNumber);
            if (user == null)
            {
                Response.StatusCode = 404;
                return "User not found";
            }

            if (user.Password != login.Password)
            {
                Response.StatusCode = 404;
                return "Incorect password";
            }

            return await _authenticationService.Login(user);
        }

        [AllowAnonymous]
        [HttpPost("UpdateToken")]
        public async Task<string> UpdateToken() 
        {
            if (!Request.Headers.TryGetValue("authorization", out StringValues jwt)) 
            {
                Response.StatusCode = 500;
                return "Jwt token not found";
            }

            string jwtString = jwt.ToString().Remove(0,7);// удаляем "Bearer " из токена
            List<Claim> claims = (await _authenticationService.GetJwtClaims(jwtString)).ToList();
            TokenPayload payload = new TokenPayload() {
                UsertId = Convert.ToInt64(claims.First(x => x.Type == ClaimTypes.UserId).Value),
                Role = claims.First(x => x.Type == ClaimTypes.Role).Value,
                JwtId = Guid.Parse(claims.First(x => x.Type == ClaimTypes.JwtId).Value),
                RefreshToken = claims.First(x => x.Type == ClaimTypes.RefreshToken).Value
            };

            return await _authenticationService.Update(payload);
        }

        [Authorize]
        [HttpGet("GetAllSessions")]
        public async Task<ICollection<SessionDTO>> GetAllSessions()
            => await _sessionRepository.GetAll(Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));

        [Authorize]
        [HttpPost("DeleteSession")]
        public async Task<string> DeleteSession(long sessionId)
        {
            Session session = await _sessionRepository.FindSessionOrDefault(sessionId);
            if (session == null)
            {
                Response.StatusCode = 404;
                return "Not found";
            }
            if(session.UserId != Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value))
            {
                Response.StatusCode = 401;
                return "Unauthorized";
            }

            _authenticationService.DeleteSession(sessionId);

            return "Success";
        }
    }
}
