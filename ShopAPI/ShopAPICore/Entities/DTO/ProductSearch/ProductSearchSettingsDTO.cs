namespace ShopAPICore.Entities.DTO.ProductSearch;

public class ProductSearchSettingsDTO
{
    public int? FirstRangePoint { get; set; }
    public int? EndRangePoint { get; set; }
    public string? Name { get; set; }
    public bool InStock { get; set; } = true;
    public ICollection<int>? TagIds { get; set; }
}