using ShopApiCore.Entities.DTO.Comment;
using System.Diagnostics.Eventing.Reader;

namespace ShopApiCore.Interfaces.Repository
{
    public interface ICommentRepository
    {
        Task Create(CreateCommentDTO creatingSettings, long userId);
        Task Edit(EditCommentDTO edittingSettings, long userId);
        Task Delete(int productId, long userId);
        Task<ICollection<CommentDTO>> Get(GetCommentsDTO gettingSettings);
    }
}
