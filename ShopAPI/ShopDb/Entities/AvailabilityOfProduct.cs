using System.ComponentModel.DataAnnotations;

namespace ShopDb.Entities
{
    public class AvailabilityOfProduct
    {
        [Key]
        public string Sku { get; set; }
        public int Count { get; set; }
        public string Size { get; set; }
        public float Cost { get; set; }

        public int PackegeSizeId {  get; set; }
        public PackegeSize PackegeSize { get; set; }
        public int Weight {  get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public ICollection<UserShoppingCartItem> UserShoppingCartItems { get; set; }
    }
}
