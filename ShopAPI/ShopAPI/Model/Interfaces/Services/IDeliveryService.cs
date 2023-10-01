using ShopDb.Entities;

namespace ShopApi.Model.Interfaces.Services
{
    public interface IDeliveryService
    {
        Task TransferToDelivery(Order order);
    }
}
