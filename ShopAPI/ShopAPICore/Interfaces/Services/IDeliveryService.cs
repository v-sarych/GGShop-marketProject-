namespace ShopAPICore.Interfaces.Services;

public interface IDeliveryService
{
    Task TransferToDelivery(Guid orderId);
    Task<float> CalculateDeliveryCost(string deliveryInfo);
}