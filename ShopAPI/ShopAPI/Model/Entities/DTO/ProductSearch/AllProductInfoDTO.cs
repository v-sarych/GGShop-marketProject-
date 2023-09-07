using ShopApi.Model.Entities.DTO.ProductAvailability;
using ShopApi.Model.Entities.DTO.Tag;
using ShopDb.Entities;

namespace ShopApi.Model.Entities.DTO.ProductSearch
{
    public class AllProductInfoDTO
    {
        public int Id { get; set; }
        public int PlaceInSearch { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotosFolder { get; set; }
        public bool CanBeFound { get; set; }

        public ICollection<TagDTO> Tags { get; set; }
        public ICollection<GetProductAvailabilityDTO> AvailabilitisOfProduct { get; set; }
    }
}
