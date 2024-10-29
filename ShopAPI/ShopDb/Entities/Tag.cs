namespace ShopDb.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = "tag";

    public ICollection<Product>? Products { get; set; }

    public ICollection<ProductTag> ProductsTag { get; set; }
}