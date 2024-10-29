using AutoMapper;
using Identity.Core.Model.Entities.DTO;
using Identity.Core.Model.Entities.Identity;
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
