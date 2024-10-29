using AutoMapper;
using ShopAPICore.Entities.DTO.Tag;
using ShopDb.Entities;

namespace ShopAPICore.Mapping.Profiles.DTO;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateProjection<Tag, TagDTO>();
    }
}