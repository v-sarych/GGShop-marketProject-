namespace ShopApiCore.Entities.DTO.ProductAvailability
{
    public class CreateProductAvailabilityDTO
    {
        public string Sku {  get; set; }
        public int Count { get; set; }
        public string Size { get; set; } = "without size";
        public float Cost { get; set; }
        public int ProductId { get; set; }

        public int Weight {  get; set; }
        public int PackageSizeId {  get; set; }
    }
}
