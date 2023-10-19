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
            CreateProjection<User, OrderUserInfoDTO>();

            CreateMap<UpdateUserInfoDTO, User>();
        }
    }
}
