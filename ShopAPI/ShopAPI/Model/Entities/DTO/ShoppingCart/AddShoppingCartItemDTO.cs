namespace ShopApi.Model.Entities.DTO.ShoppingCart
{
    public class AddShoppingCartItemDTO
    {
        public int ProductId { get; set; }
        public string? Size { get; set; }
        public int Count { get; set; }
    }
}
