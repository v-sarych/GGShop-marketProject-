using ShopAPICore.Entities.DTO.ProductControll;

namespace ShopAPICore.Interfaces.Repository;

public interface IProductControllRepository
{
    Task<int> Create();
    Task Update(UpdateProductDTO productDTO);
}