using System.ComponentModel.DataAnnotations;

namespace ShopDb.Entities;

public class PackageSize
{
    [Key]
    public int Id { get; set; }
    public int Width { get; set; } = 0;
    public int Height { get; set; } = 0;
    public int Length { get; set; } = 0;
}