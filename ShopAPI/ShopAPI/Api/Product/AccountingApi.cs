using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApiCore.Entities.DTO.ProductAvailability;
using ShopApiCore.Interfaces.Repository;
using ShopDb.Entities;

namespace ShopApiServer.Api.Product
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/Product/Accounting")]
    [ApiController]
    public class AccountingApi : ControllerBase
    {
        private readonly IProductAvailabilityRepository _productAvailabilityRepository;

        public AccountingApi(IProductAvailabilityRepository productAvailabilityRepository)
            => _productAvailabilityRepository = productAvailabilityRepository;

        [HttpPost("Create")]
        public async Task<int> Create(CreateProductAvailabilityDTO settings)
            => await _productAvailabilityRepository.Create(settings);

        [HttpPatch("Update")]
        public async Task Update(UpdateProductAvailabilityDTO settings)
            => await _productAvailabilityRepository.Update(settings);
    }
}
