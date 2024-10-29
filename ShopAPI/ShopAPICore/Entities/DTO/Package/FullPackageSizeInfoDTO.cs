namespace ShopAPICore.Entities.DTO.Package;

public class FullPackageSizeInfoDTO
{
    public int Id { get; set; }
    public int Width { get; set; } = 0;
    public int Height { get; set; } = 0;
    public int Length { get; set; } = 0;
}