using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Model.Entities.DTO.ProductAvailability;
using ShopApi.Model.Interfaces.Repository;
using ShopDb.Entities;

namespace ShopApi.Api.Product
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
