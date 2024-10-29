using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPICore.Entities.DTO.Package;
using ShopAPICore.Entities.DTO.ProductAvailability;
using ShopAPICore.Interfaces.Repository;
using ShopDb.Enums;

namespace ShopApiServer.Api.Product;

[Authorize(Roles = Roles.Admin)]
[Route("api/Product/Accounting")]
[ApiController]
public class AccountingApi : ControllerBase
{
    private readonly IProductAvailabilityRepository _productAvailabilityRepository;

    public AccountingApi(IProductAvailabilityRepository productAvailabilityRepository)
        => _productAvailabilityRepository = productAvailabilityRepository;

    /// <response code="200">Sucess</response>
    /// <response code="400">AlreadyExist</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Create")]
    public async Task Create(CreateProductAvailabilityDTO settings)
        => await _productAvailabilityRepository.Create(settings);

    [HttpPatch("Update")]
    public async Task Update(UpdateProductAvailabilityDTO settings)
        => await _productAvailabilityRepository.Update(settings);


    /// <response code="200">Sucess</response>
    /// <response code="400">AlreadyExist</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("PackageSizes/Create")]
    public async Task<int> CreatePackageSize(PackageSizeDTO packageSize)
        => await _productAvailabilityRepository.CreatepackageSize(packageSize);

    [HttpDelete("PackageSizes/Delete")]
    public async Task DeletePackageSize(int id)
        => await _productAvailabilityRepository.DeletePackageSize(id);

    [HttpGet("PackageSizes/Get")]
    public async Task<ICollection<FullPackageSizeInfoDTO>> GetPackageSize()
        => await _productAvailabilityRepository.GetPackageSizes();
}