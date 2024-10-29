using ShopAPICore.Entities.DTO.ProductAvailability;
using ShopAPICore.Entities.DTO.Tag;

namespace ShopAPICore.Entities.DTO.ProductSearch;

public class AllProductInfoDTO
{
    public int Id { get; set; }
    public int PlaceInSearch { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CanBeFound { get; set; }

    public ICollection<TagDTO> Tags { get; set; }
    public ICollection<GetProductAvailabilityDTO> AvailabilitisOfProduct { get; set; }
}