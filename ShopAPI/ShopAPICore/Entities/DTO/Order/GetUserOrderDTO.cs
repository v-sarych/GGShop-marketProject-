using ShopApiCore.Entities.DTO.OrderItem;
using ShopDb.Entities;

namespace ShopApiCore.Entities.DTO.Order
{
    public class GetUserOrderDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string AdditionalOrderInfo { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Status { get; set; }
        public bool IsPaidFor { get; set; }
        public float Cost { get; set; }

        public ICollection<GetOrderItemDTO> OrderItems { get; set; }
    }
}
