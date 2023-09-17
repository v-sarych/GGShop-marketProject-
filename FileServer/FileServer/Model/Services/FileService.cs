using FileServer.Model.Entities;
using FileServer.Model.Interfaces;
using System.IO.Compression;

namespace FileServer.Model.Services
{
    public class FileService : IFileService
    {

        public async Task Save(ICollection<AddingFile> files, string folderPath)
        {
            foreach (var file in files)
            {
                string path = Path.Combine(folderPath, $"{ file.Name + Path.GetExtension(file.File.FileName) }");

                using (var stream = new FileStream(path, FileMode.Create))
                    await file.File.CopyToAsync(stream);
            }
        }

        public async Task Delete(string path)
             => File.Delete(path);

        public async Task<string> CreateOrFindDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        public async Task<MemoryStream> CompressToZip(string[] paths)
        {
            MemoryStream stream = new MemoryStream();

            using(var zip = new ZipArchive(stream, ZipArchiveMode.Create, true))
                foreach (var path in paths)
                    zip.CreateEntryFromFile(path, path.Split(new char[] { '\\' }).Last());

            stream.Position = 0;
            return stream;
        }

        public async Task<MemoryStream> CompressToZip(string[] paths, string[] fileNamesWithoutExtentions)
        {
            MemoryStream stream = new MemoryStream();

            using (var zip = new ZipArchive(stream, ZipArchiveMode.Create, true))
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    string name = fileNamesWithoutExtentions[i] + "." + (paths[i].Split(new char[] { '\\' }).Last()).Split(new char[] { '.' }).Last();
                    zip.CreateEntryFromFile(paths[i], name);
                }
            }

            stream.Position = 0;
            return stream;
        }
    }
}
