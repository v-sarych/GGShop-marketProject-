using ShopDb.Entities;

namespace ShopApiCore.Interfaces.Services
{
    public interface IDeliveryService
    {
        Task TransferToDelivery(Guid orderId);
    }
}
