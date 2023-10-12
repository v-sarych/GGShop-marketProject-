using ShopApiCore.Entities.DTO.Payments;

namespace ShopApiCore.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<string> CreateAndAuthorizePayment(PaymentInfoDTO info);
    }
}
