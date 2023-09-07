namespace ShopDb.Entities
{
    public class OrderItem
    {
        public string? Size { get; set; }
        public float Cost { get; set; } = 0;
        public int Count { get; set; } = 1;

        public Guid OrderId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
