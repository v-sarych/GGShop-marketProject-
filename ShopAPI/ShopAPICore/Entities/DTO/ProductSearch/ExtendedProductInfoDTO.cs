using ShopAPICore.Entities.DTO.ProductAvailability;
using ShopAPICore.Entities.DTO.Tag;

namespace ShopAPICore.Entities.DTO.ProductSearch;

public class ExtendedProductInfoDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<GetProductAvailabilityDTO> AvailabilitisOfProduct { get; set; }
    public ICollection<TagDTO> Tags { get; set; }
}