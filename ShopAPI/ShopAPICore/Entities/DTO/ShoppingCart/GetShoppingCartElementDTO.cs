using ShopApiCore.Entities.DTO.ProductSearch;
using ShopDb.Entities;

namespace ShopApiCore.Entities.DTO.ShoppingCart
{
    public class GetShoppingCartElementDTO
    {
        public long Id { get; set; }
        public string Size { get; set; }
        public int Count { get; set; }
        public float Cost { get; set; }

        public SimpleProductDTO Product { get; set; }
    }
}
