namespace ShopApi.Model.Entities.DTO.ProductAvailability
{
    public class CreateProductAvailabilityDTO
    {
        public int Count { get; set; }
        public string Size { get; set; } = "without size";
        public float Cost { get; set; }
        public int ProductId { get; set; }
    }
}
