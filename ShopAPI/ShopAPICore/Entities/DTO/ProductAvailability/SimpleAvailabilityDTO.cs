namespace ShopAPICore.Entities.DTO.ProductAvailability;

public class SimpleAvailabilityDTO
{
    public string Sku { get; set; }
    public string Size { get; set; }
    public float Cost { get; set; }
    public int Count { get; set; }
}