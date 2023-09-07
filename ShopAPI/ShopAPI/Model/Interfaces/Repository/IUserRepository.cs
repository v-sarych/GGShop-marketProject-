using ShopApi.Model.Entities.DTO.User;
using ShopDb.Entities;

namespace ShopApi.Model.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<GetUserDTO> Get(long id);
        Task UpdateInfo(UpdateUserInfoDTO updateInfo, long id);
    }
}
