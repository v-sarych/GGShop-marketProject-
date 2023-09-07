using IdentityServer.Model.Entities.DTO;
using IdentityServer.Model.Entities.Identity;
using ShopDb.Entities;

namespace IdentityServer.Model.interfaces.Repositories
{
    public interface ISessionRepository
    {
        Task<ICollection<SessionDTO>> GetAll(long userId);
        Task<bool> TryFind(Guid jwtId);
        Task<Session> FindSessionOrDefault(long id);
        Task<Session> Create(SessionCreateInfo info);
        Task<Session> Update(Guid jwtId, string refreshToken, string newRefreshToken);
        Task Delete(long sessionId);
    }
}
