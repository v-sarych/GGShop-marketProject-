namespace ShopApiCore.Entities.DTO.Comment
{
    public class GetCommentsDTO
    {
        public int? FirstRangePoint { get; set; }
        public int? EndRangePoint { get; set; }
        public int? Stars { get; set; }
        public long? UserId { get; set; }
        public int? ProductId { get; set; }
    }
}
