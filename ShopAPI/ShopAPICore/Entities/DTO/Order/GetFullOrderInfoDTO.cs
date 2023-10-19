using ShopApiCore.Entities.DTO.OrderItem;
using ShopApiCore.Entities.DTO.User;

namespace ShopApiCore.Entities.DTO.Order
{
    public class GetFullOrderInfoDTO
    {
        public Guid Id { get; set; }
        public string AdditionalOrderInfo { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Status { get; set; }
        public bool IsPaidFor { get; set; }
        public float Cost { get; set; }

        public OrderUserInfoDTO User { get; set; }

        public ICollection<GetOrderItemDTO> OrderItems { get; set; }
    }
}
