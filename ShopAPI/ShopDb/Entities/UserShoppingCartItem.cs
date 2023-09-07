using System.ComponentModel.DataAnnotations;

namespace ShopDb.Entities
{
    public class UserShoppingCartItem
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Size { get; set; } = "without size";
        public int Count { get; set; } = 1;

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
