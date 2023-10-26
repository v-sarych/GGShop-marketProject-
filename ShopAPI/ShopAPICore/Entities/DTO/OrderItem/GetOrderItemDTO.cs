using ShopApiCore.Entities.DTO.ProductAvailability;
using ShopApiCore.Entities.DTO.ProductSearch;

namespace ShopApiCore.Entities.DTO.OrderItem
{
    public class GetOrderItemDTO
    {
        public int Count { get; set; }
        public float Cost { get; set; }

        public AvailabilityForGetOrderItemDTO AvailabilityOfProduct {  get; set; }
        public SimpleProductDTO Product { get; set; }
    }
}
