using ShopAPICore.Entities.DTO.ProductSearch;

namespace ShopAPICore.Entities.DTO.ProductAvailability;

public class AvailabilityForGetShoppingCartDTO
{
    public string Size {  get; set; }

    public SimpleProductDTO Product { get; set; }
}