using ShopApi.Model.Entities.DTO.OrderItem;

namespace ShopApi.Model.Entities.DTO.Order
{
    public class CreateOrderDTO
    {
        public string DeliveryAddress { get; set; }

        public ICollection<CreateOrderItemDTO> OrderItems { get; set; }
    }
}
