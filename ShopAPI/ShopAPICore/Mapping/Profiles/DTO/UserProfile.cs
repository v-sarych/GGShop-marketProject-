using AutoMapper;
using ShopApiCore.Entities.DTO.User;
using ShopDb.Entities;

namespace ShopApiCore.Mapping.Profiles.DTO
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateProjection<User, GetUserDTO>();
            CreateProjection<User, SimpleUserDTO>();

            CreateMap<UpdateUserInfoDTO, User>();
        }
    }
}
