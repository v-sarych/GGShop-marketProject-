
using IdentityServer.Model.Entities.Identity;
using IdentityServer.Model.Exeptions;
using IdentityServer.Model.interfaces;
using IdentityServer.Model.interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using ShopDb.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;

namespace IdentityServer.Model
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtCreator _jwtCreator;
        private readonly ISessionRepository _sessionRepository;

        public AuthenticationService(IJwtCreator jwtCreator, ISessionRepository sessionRepository)
        {
            _jwtCreator = jwtCreator;
            _sessionRepository = sessionRepository;
        }

        public async Task<string> Login(UserIdentity user)
        {
            Guid sessionId = Guid.NewGuid();

            string newRefreshToken = _createRandomToken();
            Session session = await _sessionRepository.Create(new SessionCreateInfo()
            {
                UserId = user.Id,
                Id = sessionId,
                Role = user.Role,
                RefreshToken = newRefreshToken
            });

            JwtSecurityToken jwt =  _jwtCreator.Create(new TokenPayload()
            {
                Id = session.Id,
                RefreshToken = newRefreshToken,
                UsertId = session.UserId,
                Role = session.Role
            });

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<string> Update(TokenPayload payload)
        {
            if (!(await _sessionRepository.TryFind(payload.Id)))
                throw new NotFoundException();

            string newRefreshToken = _createRandomToken();
            Session session = await _sessionRepository.Update(payload.Id, payload.RefreshToken, newRefreshToken);

            payload.RefreshToken = newRefreshToken;

            JwtSecurityToken jwt = _jwtCreator.Create(payload);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<ICollection<Claim>> GetJwtClaims(string jwtString)
            => new JwtSecurityTokenHandler().ReadJwtToken(jwtString).Claims.ToList();

        public async Task DeleteSession(Guid id)
            => await _sessionRepository.Delete(id);

        private string _createRandomToken()
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
