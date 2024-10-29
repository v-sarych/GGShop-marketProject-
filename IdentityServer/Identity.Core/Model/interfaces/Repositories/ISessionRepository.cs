using Identity.Core.Model.Entities.DTO;
using Identity.Core.Model.Entities.Identity;
using ShopDb.Entities;

namespace Identity.Core.Model.interfaces.Repositories;

public interface ISessionRepository
{
    Task<ICollection<SessionDTO>> GetAll(long userId);
    Task<bool> TryFind(Guid Id);
    Task<bool> TryFind(Guid Id, string refreshTokenHash);
    Task<Session> FindSessionOrDefault(Guid id);
    Task<Session> Create(SessionCreateInfo info);
    Task<Session> Update(Guid Id, string newRefreshToken);
    Task Delete(Guid sessionId);
}