namespace ShopApiCore.Entities.DTO.ProductAvailability
{
    public class UpdateProductAvailabilityDTO
    {
        public string Sku{ get; set; }
        public int? Count { get; set; }
        public float? Cost { get; set; }

        public int? Weight { get; set; }
        public int? PackageId {  get; set; }
    }
}
