namespace ShopApiCore.Entities.DTO.OrderItem
{
    public class CreateOrderItemDTO
    {
        public string Sku {  get; set; }
        public string Size { get; set; } = "without size";
        public int Count { get; set; }
        public int ProductId { get; set; }
    }
}
