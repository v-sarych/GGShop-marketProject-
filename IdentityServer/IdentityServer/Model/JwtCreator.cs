using IdentityServer.Model.interfaces;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer.Model.Entities.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

using ClaimTypes = IdentityServer.Model.Entities.Identity.ClaimTypes;

namespace IdentityServer.Model
{
    public class JwtCreator : IJwtCreator
    {
        private readonly JwtConfiguration _configuration;
        public JwtCreator(JwtConfiguration configuration) => _configuration = configuration;
        public JwtSecurityToken Create(TokenPayload payload)
        {
            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.UserId, Convert.ToString(payload.UsertId)),
                new Claim(ClaimTypes.Role, payload.Role),
                new Claim(ClaimTypes.SessionId, payload.Id.ToString()),
                new Claim(ClaimTypes.RefreshToken, payload.RefreshToken)

            };

            return new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: "site",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_configuration.LifeTimeInMinutes),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.SymetricKey)), SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
