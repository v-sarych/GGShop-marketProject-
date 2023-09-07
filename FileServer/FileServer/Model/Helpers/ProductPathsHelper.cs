using FileServer.Model.Entities;
using FileServer.Model.Interfaces;

namespace FileServer.Model.Helpers
{
    public class ProductPathsHelper : IProductPathsHelper
    {
        private readonly PathConfiguration _pathConfiguration;

        public ProductPathsHelper(PathConfiguration pathConfiguration) => _pathConfiguration = pathConfiguration;

        public string[] GetAllProductImagesPaths(int productId)
            => Directory.GetFiles(GetProductFolderPath() + $"\\{productId}");

        public string[] GetMainProductsImagesPaths(int[] productIds)
        {
            string[] result = new string[productIds.Length];

            for(int i = 0; i < productIds.Length; i++) 
                result[i] = GetProductFilePathByName(productIds[i], _pathConfiguration.ProductMainImageName);

            return result;
        }

        public string GetProductFilePathByName(int productId, string fileName)
            => Directory.GetFiles(GetProductFolderPath() + $"\\{ productId }")
                    .First(x => Path.GetFileNameWithoutExtension(x) == fileName);

        public string GetProductFolderPath()
            => _pathConfiguration.StoragePath + _pathConfiguration.ProductSubpath;
    }
}
