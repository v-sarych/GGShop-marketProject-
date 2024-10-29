using ShopAPICore.Entities.DTO.ProductAvailability;

namespace ShopAPICore.Entities.DTO.ProductSearch;

public class ProductSearchResultDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<SimpleAvailabilityDTO> AvailabilitisOfProduct { get; set; }
}