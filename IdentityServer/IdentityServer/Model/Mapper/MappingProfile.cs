using AutoMapper;
using IdentityServer.Model.Entities.Identity;
using IdentityServer.Model.Entities.DTO;
using ShopDb.Entities;

namespace IdentityServer.Model.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserIdentity>();
            CreateMap<Session, SessionDTO>();
        }
    }
}
