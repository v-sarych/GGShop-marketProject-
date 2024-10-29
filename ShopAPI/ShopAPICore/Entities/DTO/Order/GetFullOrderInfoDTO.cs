using ShopAPICore.Entities.DTO.OrderItem;
using ShopAPICore.Entities.DTO.User;

namespace ShopAPICore.Entities.DTO.Order;

public class GetFullOrderInfoDTO
{
    public Guid Id { get; set; }
    public string? DeliveryInfo { get; set; }
    public string? AdditionalOrderInfo { get; set; }
    public DateTime DateOfCreation { get; set; }
    public string Status { get; set; }
    public bool IsPaidFor { get; set; }
    public float AdditionalFees { get; set; }
    public float Cost { get; set; }

    public OrderUserInfoDTO User { get; set; }

    public ICollection<GetOrderItemDTO> OrderItems { get; set; }
}