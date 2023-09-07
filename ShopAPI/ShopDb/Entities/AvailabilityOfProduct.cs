using System.ComponentModel.DataAnnotations;

namespace ShopDb.Entities
{
    public class AvailabilityOfProduct
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; }
        public string Size { get; set; }
        public float Cost { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
