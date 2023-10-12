using ShopApiCore.Entities.DTO.ProductAvailability;

namespace ShopApiCore.Interfaces.Repository
{
    public interface IProductAvailabilityRepository
    {
        Task<int> Create(CreateProductAvailabilityDTO settings);
        Task Update(UpdateProductAvailabilityDTO settings);
    }
}
