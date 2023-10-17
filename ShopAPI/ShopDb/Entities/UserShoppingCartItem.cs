using System.ComponentModel.DataAnnotations;

namespace ShopDb.Entities
{
    public class UserShoppingCartItem
    {
        public long UserId { get; set; }
        public int Count { get; set; } = 1;

        public string Sku { get; set; }
        public AvailabilityOfProduct AvailabilityOfProduct { get; set; }
    }
}
