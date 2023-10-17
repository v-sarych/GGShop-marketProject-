namespace ShopApiCore.Entities.DTO.ProductAvailability
{
    public class GetProductAvailabilityDTO
    {
        public string Sku { get; set; }
        public int Count { get; set; }
        public string Size { get; set; }
        public float Cost { get; set; }
    }
}
