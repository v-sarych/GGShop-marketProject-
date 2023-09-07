using IdentityServer.Model.Entities;
using IdentityServer.Model.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace IdentityServer.Model.interfaces
{
    public interface IJwtCreator
    {
        JwtSecurityToken Create(TokenPayload payload);
    }
}
