using AutoMapper;
using ShopApi.Model.Entities.DTO.Comment;
using ShopDb.Entities;

namespace ShopApi.Model.Mapping.Profiles.DTO
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateProjection<Comment, CommentDTO>();
        }
    }
}
