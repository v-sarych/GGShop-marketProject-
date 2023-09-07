using ShopApi.Model.Entities.DTO.ProductControll;

namespace ShopApi.Model.Interfaces.Repository
{
    public interface IProductControllRepository
    {
        Task<int> Create();
        Task Update(UpdateProductDTO productDTO);
    }
}
