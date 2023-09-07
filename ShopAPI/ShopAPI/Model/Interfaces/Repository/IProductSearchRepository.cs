using ShopApi.Model.Entities.DTO.ProductSearch;
using ShopApi.Model.Entities.DTO.SearchResults;
using ShopApi.Model.Entities.DTO.Tag;

namespace ShopApi.Model.Interfaces.Repository
{
    public interface IProductSearchRepository
    {
        Task<ICollection<ProductSearchResultDTO>> Search(ProductSearchSettingsDTO settings);
        Task<ICollection<SimpleProductDTO>> SearchWithoutAccounting(ProductSearchSettingsDTO settings);

        Task<AllProductInfoDTO> GetAllProductInfo(int id);
        Task<ExtendedProductInfoDTO> GetExtendedProductInfo(int id);
    }
}
