using Identity.Core.Model.Entities.DTO;
using Identity.Core.Model.Entities.Identity;

namespace Identity.Core.Model.interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> TryFindUser(string phoneNumber);
    Task<UserIdentity> FindUserOrDefault(string phoneNumber);
    Task<UserIdentity> Create(RegisterUserDTO registerUser);
    Task UpdatePassword(long id, string newPassword);
    Task UpdatePhoneNumber(long id, string newPhoneNumber);
    Task Delete(long id);
}