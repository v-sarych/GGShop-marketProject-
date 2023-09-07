using IdentityServer.Model.Entities.Identity;
using System.Security.Claims;

namespace IdentityServer.Model.interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Login(UserIdentity user);
        Task<string> Update(TokenPayload payload);
        Task<ICollection<Claim>> GetJwtClaims(string jwtString);
        Task DeleteSession(long id);
    }
}
