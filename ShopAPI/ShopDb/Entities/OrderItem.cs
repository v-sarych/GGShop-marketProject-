namespace ShopDb.Entities;

public class OrderItem
{
    public float Cost { get; set; } = 0;
    public int Count { get; set; } = 1;

    public string Sku {  get; set; }
    public AvailabilityOfProduct AvailabilityOfProduct { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}