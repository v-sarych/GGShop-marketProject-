using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApiCore.Entities.DTO.ProductControll;
using ShopApiCore.Interfaces.Repository;
using ShopDb.Entities;

namespace ShopApiServer.Api.Product
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/Product")]
    [ApiController]
    public class ProductControllApi : ControllerBase
    {
        private readonly IProductControllRepository _productControllRepository;

        public ProductControllApi(IProductControllRepository productControllRepository)
            => _productControllRepository = productControllRepository;

        [HttpPost("Create")]
        public async Task<int> Create()
            => await _productControllRepository.Create();

        [HttpPatch("Update")]
        public async Task Update(UpdateProductDTO productDTO)
            => await _productControllRepository.Update(productDTO);
    }
}
