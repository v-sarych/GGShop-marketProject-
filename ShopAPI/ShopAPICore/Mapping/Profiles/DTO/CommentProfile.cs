using AutoMapper;
using ShopApiCore.Entities.DTO.Comment;
using ShopDb.Entities;

namespace ShopApiCore.Mapping.Profiles.DTO
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateProjection<Comment, CommentDTO>();
        }
    }
}
