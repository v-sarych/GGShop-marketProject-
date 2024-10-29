using ShopAPICore.Entities.DTO.Package;
using ShopAPICore.Entities.DTO.ProductAvailability;

namespace ShopAPICore.Interfaces.Repository;

public interface IProductAvailabilityRepository
{
    Task Create(CreateProductAvailabilityDTO settings);
    Task Update(UpdateProductAvailabilityDTO settings);

    Task<int> CreatepackageSize(PackageSizeDTO packageSize);
    Task DeletePackageSize(int id);
    Task<ICollection<FullPackageSizeInfoDTO>> GetPackageSizes();
}