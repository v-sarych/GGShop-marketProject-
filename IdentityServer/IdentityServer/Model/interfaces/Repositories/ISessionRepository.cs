using IdentityServer.Model.Entities.DTO;
using IdentityServer.Model.Entities.Identity;
using ShopDb.Entities;

namespace IdentityServer.Model.interfaces.Repositories
{
    public interface ISessionRepository
    {
        Task<ICollection<SessionDTO>> GetAll(long userId);
        Task<bool> TryFind(Guid Id);
        Task<Session> FindSessionOrDefault(Guid id);
        Task<Session> Create(SessionCreateInfo info);
        Task<Session> Update(Guid Id, string refreshToken, string newRefreshToken);
        Task Delete(Guid sessionId);
    }
}
