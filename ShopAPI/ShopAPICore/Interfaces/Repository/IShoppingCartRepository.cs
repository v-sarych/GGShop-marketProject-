using ShopApiCore.Entities.DTO.ShoppingCart;

namespace ShopApiCore.Interfaces.Repository
{
    public interface IShoppingCartRepository
    {
        Task<ICollection<GetShoppingCartElementDTO>> Get(long id);
        Task<long> AddItem(AddShoppingCartItemDTO item, long userId);
        Task UpdateItemCount(UpdateShoppingCartItemDTO itemWithChanges, long userId);
        Task RemoveItem(long itemId, long userId);
    }
}
