namespace ShopApiCore.Entities.DTO.Order
{
    public class OrderSearchSettingsDTO
    {
        public int? FirstRangePoint { get; set; }
        public int? EndRangePoint { get; set; }
        public string? OrderStatus { get; set; }
        public long? UserId { get; set; }
        public int? ProductId { get; set; }
    }
}
