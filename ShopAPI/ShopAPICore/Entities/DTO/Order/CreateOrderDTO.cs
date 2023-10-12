using ShopApiCore.Entities.DTO.OrderItem;

namespace ShopApiCore.Entities.DTO.Order
{
    public class CreateOrderDTO
    {
        public string DeliveryAddress { get; set; }

        public ICollection<CreateOrderItemDTO> OrderItems { get; set; }
    }
}
