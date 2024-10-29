using System.ComponentModel.DataAnnotations;

namespace ShopDb.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    public int PlaceInSearch { get; set; }
    public string Name { get; set; } = "Noname";
    public string? Description { get; set; }
    public bool CanBeFound { get; set; } = false;

    public ICollection<Tag>? Tags { get; set; }
    public ICollection<AvailabilityOfProduct>? AvailabilitisOfProduct { get; set; }
    public ICollection<Comment> Comments { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }

    public ICollection<ProductTag> ProductTags { get; set; }
}