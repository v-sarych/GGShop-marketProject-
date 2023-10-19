namespace ShopDb.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string AdditionalOrderInfo { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Status { get; set; } = OrderStatuses.Created;
        public bool IsPaidFor { get; set; } = false;
        public float Cost { get; set; } = 0;

        public long UserId { get; set; }
        public User User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
