namespace FileServer.Model.Entities
{
    public record PathConfiguration(
        string StoragePath, 
        string ProductSubpath,
        string ProductMainImageName
        );
}