using AutoMapper;
using ShopApi.Model.Entities.DTO.Tag;
using ShopDb.Entities;

namespace ShopApi.Model.Mapping.Profiles.DTO
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateProjection<Tag, TagDTO>();
        }
    }
}
