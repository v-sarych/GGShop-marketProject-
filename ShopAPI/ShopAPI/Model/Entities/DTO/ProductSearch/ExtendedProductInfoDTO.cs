using ShopApi.Model.Entities.DTO.ProductAvailability;
using ShopApi.Model.Entities.DTO.Tag;

namespace ShopApi.Model.Entities.DTO.ProductSearch
{
    public class ExtendedProductInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<SimpleAvailabilityDTO> AvailabilitisOfProduct { get; set; }
        public ICollection<TagDTO> Tags { get; set; }
    }
}
