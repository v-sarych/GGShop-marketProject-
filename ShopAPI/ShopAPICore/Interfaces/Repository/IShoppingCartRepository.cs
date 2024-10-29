using ShopAPICore.Entities.DTO.ShoppingCart;

namespace ShopAPICore.Interfaces.Repository;

public interface IShoppingCartRepository
{
    Task<ICollection<GetShoppingCartElementDTO>> Get(long id);
    Task AddItem(AddShoppingCartItemDTO item, long userId);
    Task UpdateItemCount(UpdateShoppingCartItemDTO itemWithChanges, long userId);
    Task RemoveItem(string sku, long userId);
}