using ShopApiCore.Entities.DTO.ProductAvailability;

namespace ShopApiCore.Interfaces.Repository
{
    public interface IProductAvailabilityRepository
    {
        Task Create(CreateProductAvailabilityDTO settings);
        Task Update(UpdateProductAvailabilityDTO settings);

        Task CreatepackageSize(PackageSizeDTO packageSize);
        Task DeletePackageSize(int id);
        Task<ICollection<FullPackageSizeInfoDTO>> GetPackageSizes();
    }
}
