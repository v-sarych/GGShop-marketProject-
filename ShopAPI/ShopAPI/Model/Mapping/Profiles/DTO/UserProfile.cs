using AutoMapper;
using ShopApi.Model.Entities.DTO.User;
using ShopDb.Entities;

namespace ShopApi.Model.Mapping.Profiles.DTO
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
