using ShopApi.Model.Entities.DTO.ProductSearch;

namespace ShopApi.Model.Entities.DTO.OrderItem
{
    public class GetOrderItemDTO
    {
        public string Size { get; set; }
        public int Count { get; set; }
        public float Cost { get; set; }

        public SimpleProductDTO Product { get; set; }
    }
}
