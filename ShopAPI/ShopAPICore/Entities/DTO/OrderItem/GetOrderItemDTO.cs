using ShopAPICore.Entities.DTO.ProductAvailability;
using ShopAPICore.Entities.DTO.ProductSearch;

namespace ShopAPICore.Entities.DTO.OrderItem;

public class GetOrderItemDTO
{
    public int Count { get; set; }
    public float Cost { get; set; }

    public AvailabilityForGetOrderItemDTO AvailabilityOfProduct {  get; set; }
    public SimpleProductDTO Product { get; set; }
}