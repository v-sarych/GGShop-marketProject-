using ShopAPICore.Entities.DTO.ProductSearch;

namespace ShopAPICore.Interfaces.Repository;

public interface IProductSearchRepository
{
    Task<ICollection<ProductSearchResultDTO>> Search(ProductSearchSettingsDTO settings);
    Task<ICollection<SimpleProductDTO>> SearchWithoutAccounting(ProductSearchSettingsDTO settings, bool useCanBeFound);

    Task<AllProductInfoDTO> GetAllProductInfo(int id);
    Task<ExtendedProductInfoDTO> GetExtendedProductInfo(int id);
}