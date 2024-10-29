using Identity.Core.Model.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Identity.Core.Model.interfaces;

public interface IJwtCreator
{
    JwtSecurityToken Create(TokenPayload payload);
}