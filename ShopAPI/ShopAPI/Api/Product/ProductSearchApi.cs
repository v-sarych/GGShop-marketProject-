using Microsoft.AspNetCore.Mvc;
using ShopDb.Entities;
using ShopDb;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApi.Model.Entities.DTO;
using ShopApi.Model.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using ShopApi.Model.Entities.DTO.SearchResults;
using ShopApi.Model.Entities.DTO.ProductSearch;
using ShopApi.Model.Repositories;

namespace ShopApi.Api.Product
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
        public async Task<ICollection<SimpleProductDTO>> SearchWithoutAccounting(ProductSearchSettingsDTO searchSettings)
            => await _productSearchRepository.SearchWithoutAccounting(searchSettings);

        [HttpGet("GetExtendedInfo")]
        public async Task<ExtendedProductInfoDTO> GetExtendedProductInfo(int id)
            => await _productSearchRepository.GetExtendedProductInfo(id);

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("GetAllInfo")]
        public async Task<AllProductInfoDTO> GetAllInfo(int id)
            => await _productSearchRepository.GetAllProductInfo(id);
    }
}
