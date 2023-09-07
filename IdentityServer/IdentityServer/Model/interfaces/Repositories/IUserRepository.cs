using IdentityServer.Model.Entities.DTO;
using IdentityServer.Model.Entities.Identity;

namespace IdentityServer.Model.interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> TryFindUser(string phoneNumber);
        Task<UserIdentity> FindUserOrDefault(string phoneNumber);
        Task<UserIdentity> Create(RegisterUserDTO registerUser);
        Task UpdatePassword(long id, string newPassword);
        Task UpdatePhoneNumber(long id, string newPhoneNumber);
        Task Delete(long id);
    }
}
