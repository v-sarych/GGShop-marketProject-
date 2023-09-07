using FileServer.Model.Entities;

namespace FileServer.Model.Interfaces
{
    public interface IProductPathsHelper
    {
        string GetProductFolderPath();

        string[] GetMainProductsImagesPaths(int[] productIds);
        public string GetProductFilePathByName(int productId, string fileName);

        string[] GetAllProductImagesPaths(int productId);
    }
}
