using ShopApiCore.Entities.DTO.User;

namespace ShopApiCore.Entities.DTO.Comment
{
    public class CommentDTO
    {
        public int ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Stars { get; set; }
        public string Text { get; set; }

        public SimpleUserDTO User { get; set; }
    }
}
