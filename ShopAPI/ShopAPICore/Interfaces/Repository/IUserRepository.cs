using ShopApiCore.Entities.DTO.User;
using ShopDb.Entities;

namespace ShopApiCore.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<GetUserDTO> Get(long id);
        Task UpdateInfo(UpdateUserInfoDTO updateInfo, long id);
    }
}
