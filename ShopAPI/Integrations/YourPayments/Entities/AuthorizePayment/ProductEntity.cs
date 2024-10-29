namespace Integrations.YourPayments.Entities.AuthorizePayment;

public class ProductEntity
{
    public string Name { get; set; }
    public string Sku { get; set; }
    public string UnitPrice { get; set; }
    public ushort Quantity { get; set; }
}