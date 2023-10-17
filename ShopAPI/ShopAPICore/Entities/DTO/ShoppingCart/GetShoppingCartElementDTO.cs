using ShopApiCore.Entities.DTO.ProductAvailability;
using ShopApiCore.Entities.DTO.ProductSearch;
using ShopDb.Entities;

namespace ShopApiCore.Entities.DTO.ShoppingCart
{
    public class GetShoppingCartElementDTO
    {
        public int Count { get; set; }
        public float Cost { get; set; }

        public int? AvailablуInStock { get; set; }

        public string Sku { get; set; }
        public AvailabilityForGetShoppingCartDTO AvailabilityOfProduct { get; set; }
    }
}
