using ShopApi.Model.Entities.DTO.OrderItem;

namespace ShopApi.Model.Entities.DTO.Order
{
    public class GetFullOrderInfoDTO
    {
        public Guid Id { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Status { get; set; }
        public bool IsPaidFor { get; set; }
        public float Cost { get; set; }

        public long UserId { get; set; }
        public string UserPhoneNumber { get; set; }

        public ICollection<GetOrderItemDTO> OrderItems { get; set; }
    }
}
