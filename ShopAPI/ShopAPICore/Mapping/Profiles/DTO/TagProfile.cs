using AutoMapper;
using ShopApiCore.Entities.DTO.Tag;
using ShopDb.Entities;

namespace ShopApiCore.Mapping.Profiles.DTO
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateProjection<Tag, TagDTO>();
        }
    }
}
