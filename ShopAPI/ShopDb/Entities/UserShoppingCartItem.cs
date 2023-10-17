using System.ComponentModel.DataAnnotations;

namespace ShopDb.Entities
{
    public class UserShoppingCartItem
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Count { get; set; } = 1;

        public string Sku { get; set; }
        public AvailabilityOfProduct AvailabilityOfProduct { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
