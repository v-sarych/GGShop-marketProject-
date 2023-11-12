using Microsoft.AspNetCore.Mvc;
using ShopDb.Entities;
using ShopDb;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ShopApiCore.Interfaces.Repository;
using ShopApiCore.Entities.DTO.SearchResults;
using ShopApiCore.Entities.DTO.ProductSearch;

namespace ShopApiServer.Api.Product
{
    [Route("api/Product")]
    [ApiController]
    public class ProductSearchApi : ControllerBase
    {
        private readonly IProductSearchRepository _productSearchRepository;

        public ProductSearchApi(IProductSearchRepository productSearchRepository)
            => _productSearchRepository = productSearchRepository;

        [HttpPost("Search")]
        public async Task<ICollection<ProductSearchResultDTO>> Search(ProductSearchSettingsDTO searchSettings)
            => await _productSearchRepository.Search(searchSettings);

        [HttpPost("Search/WithoutAccounting")]
        public async Task<ICollection<SimpleProductDTO>> SearchWithoutAccountingWithCanBeFound(ProductSearchSettingsDTO searchSettings)
            => await _productSearchRepository.SearchWithoutAccounting(searchSettings, true);

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Search/WithoutAccounting/All")]
        public async Task<ICollection<SimpleProductDTO>> SearchWithoutAccountingWithoutCanBeFound(ProductSearchSettingsDTO searchSettings)
            => await _productSearchRepository.SearchWithoutAccounting(searchSettings, false);

        [HttpGet("GetExtendedInfo")]
        public async Task<ExtendedProductInfoDTO> GetExtendedProductInfo(int id)
            => await _productSearchRepository.GetExtendedProductInfo(id);

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("GetAllInfo")]
        public async Task<AllProductInfoDTO> GetAllInfo(int id)
            => await _productSearchRepository.GetAllProductInfo(id);
    }
}
