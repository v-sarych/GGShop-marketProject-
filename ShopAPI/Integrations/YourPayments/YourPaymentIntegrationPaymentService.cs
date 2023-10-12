using ShopApiCore.Entities.DTO.Payments;
using ShopApiCore.Interfaces.Services;

namespace Integrations.YourPayments
{
    public class YourPaymentIntegrationPaymentService : IPaymentService
    {
        public Task<string> CreateAndAuthorizePayment(PaymentInfoDTO info)
        {
            throw new NotImplementedException();
        }
    }
}
