using ShopAPICore.Entities.DTO.ProductAvailability;

namespace ShopAPICore.Entities.DTO.ShoppingCart;

public class GetShoppingCartElementDTO
{
    public int Count { get; set; }
    public float Cost { get; set; }

    public int? AvailablуInStock { get; set; }

    public string Sku { get; set; }
    public AvailabilityForGetShoppingCartDTO AvailabilityOfProduct { get; set; }
}