namespace ShopApiCore.Entities.DTO.Comment
{
    public class CreateCommentDTO
    {
        public int ProductId { get; set; }
        public int Stars { get; set; }
        public string Text { get; set; }
    }
}
