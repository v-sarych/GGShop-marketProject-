using AutoMapper;
using ShopAPICore.Entities.DTO.Comment;
using ShopDb.Entities;

namespace ShopAPICore.Mapping.Profiles.DTO;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateProjection<Comment, CommentDTO>();
    }
}