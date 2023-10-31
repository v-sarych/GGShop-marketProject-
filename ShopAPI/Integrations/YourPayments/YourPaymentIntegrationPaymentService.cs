using ShopApiCore.Entities.DTO.Payments;
using ShopApiCore.Interfaces.Services;

namespace Integrations.YourPayments
{
    public class YourPaymentIntegrationPaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public YourPaymentIntegrationPaymentService(IHttpClientFactory httpClientFactory)
            => _httpClientFactory = httpClientFactory;
        public Task<string> CreateAndAuthorizePayment(PaymentInfoDTO info)
        {
            throw new NotImplementedException();
        }
    }
}
