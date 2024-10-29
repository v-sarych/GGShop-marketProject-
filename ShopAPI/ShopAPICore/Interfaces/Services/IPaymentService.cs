using ShopAPICore.Entities.DTO.Payments;

namespace ShopAPICore.Interfaces.Services;

public interface IPaymentService
{
    Task<string> CreateAndAuthorizePayment(PaymentInfoDTO info);
    Task<string> CheckPaymentStatus(Guid OrderId);
}