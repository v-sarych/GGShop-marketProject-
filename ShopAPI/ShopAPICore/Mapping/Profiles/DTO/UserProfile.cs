using AutoMapper;
using ShopAPICore.Entities.DTO.User;
using ShopDb.Entities;

namespace ShopAPICore.Mapping.Profiles.DTO;

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