using System.ComponentModel.DataAnnotations;

namespace ShopApiCore.Entities.DTO.ProductAvailability
{
    public class CreateProductAvailabilityDTO
    {
        [Required]
        public string Sku {  get; set; }
        [Required]
        public int Count { get; set; }
        public string Size { get; set; } = "without size";
        [Required]
        public float Cost { get; set; }
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Weight {  get; set; }
        [Required]
        public int PackageSizeId {  get; set; }
    }
}
