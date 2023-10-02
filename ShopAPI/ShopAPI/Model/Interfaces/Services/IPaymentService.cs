using ShopApi.Model.Entities.DTO.Payments;

namespace ShopApi.Model.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<string> CreateAndAuthorizePayment(PaymentInfoDTO info);
    }
}
