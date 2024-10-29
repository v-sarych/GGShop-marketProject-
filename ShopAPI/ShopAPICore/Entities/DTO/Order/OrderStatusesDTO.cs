using ShopDb.Enums;

namespace ShopAPICore.Entities.DTO.Order;

public class OrderStatusesDTO
{
    public readonly string Created = OrderStatuses.Created;
    public readonly string Cancelled = OrderStatuses.Cancelled;
    public readonly string Accepted = OrderStatuses.Accepted;
    public readonly string InDelivery = OrderStatuses.InDelivery;
}