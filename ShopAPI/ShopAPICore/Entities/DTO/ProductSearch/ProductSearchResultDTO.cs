using ShopApiCore.Entities.DTO.ProductAvailability;

namespace ShopApiCore.Entities.DTO.SearchResults
{
    public class ProductSearchResultDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<SimpleAvailabilityDTO> AvailabilitisOfProduct { get; set; }
    }
}
