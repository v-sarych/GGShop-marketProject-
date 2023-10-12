using ShopApiCore.Entities.DTO.ProductSearch;
using ShopApiCore.Entities.DTO.SearchResults;
using ShopApiCore.Entities.DTO.Tag;

namespace ShopApiCore.Interfaces.Repository
{
    public interface IProductSearchRepository
    {
        Task<ICollection<ProductSearchResultDTO>> Search(ProductSearchSettingsDTO settings);
        Task<ICollection<SimpleProductDTO>> SearchWithoutAccounting(ProductSearchSettingsDTO settings);

        Task<AllProductInfoDTO> GetAllProductInfo(int id);
        Task<ExtendedProductInfoDTO> GetExtendedProductInfo(int id);
    }
}
