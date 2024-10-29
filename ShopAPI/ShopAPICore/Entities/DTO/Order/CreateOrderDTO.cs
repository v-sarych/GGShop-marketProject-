using ShopAPICore.Entities.DTO.OrderItem;

namespace ShopAPICore.Entities.DTO.Order;

public class CreateOrderDTO
{
    public string? AdditionalOrderInfo { get; set; }
    public string? DeliveryInfo { get; set; }
    public string Type { get; set; }

    public ICollection<CreateOrderItemDTO> OrderItems { get; set; }
}