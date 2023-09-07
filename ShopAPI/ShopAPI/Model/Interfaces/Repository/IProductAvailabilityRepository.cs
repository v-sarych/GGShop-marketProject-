using ShopApi.Model.Entities.DTO.ProductAvailability;

namespace ShopApi.Model.Interfaces.Repository
{
    public interface IProductAvailabilityRepository
    {
        Task<int> Create(CreateProductAvailabilityDTO settings);
        Task Update(UpdateProductAvailabilityDTO settings);
    }
}
