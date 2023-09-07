using FileServer.Model.Entities;

namespace FileServer.Model.Interfaces
{
    public interface IFileService
    {
        Task<string> CreateOrFindDirectory(string path);
        Task Save(ICollection<AddingFile> files, string path);
        Task Delete(string path);

        Task<MemoryStream> CompressToZip(string[] paths);
        Task<MemoryStream> CompressToZip(string[] paths, string[] fileNamesWithoutExtentions);
    }
}
