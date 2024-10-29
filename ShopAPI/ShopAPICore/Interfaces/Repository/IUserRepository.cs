using ShopAPICore.Entities.DTO.User;

namespace ShopAPICore.Interfaces.Repository;

public interface IUserRepository
{
    Task<GetUserDTO> Get(long id);
    Task UpdateInfo(UpdateUserInfoDTO updateInfo, long id);
}