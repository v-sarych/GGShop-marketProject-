using Integrations.YourPayments.Interfaces;
using ShopApiCore.Entities.DTO.Payments;
using ShopApiCore.Interfaces.Services;

namespace Integrations.YourPayments
{
    public class YourPaymentIntegrationPaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPaymentDataCreator _paymentDataCreator;
        public YourPaymentIntegrationPaymentService(IHttpClientFactory httpClientFactory, IPaymentDataCreator paymentDataCreator)
            => (_httpClientFactory, _paymentDataCreator) = (httpClientFactory, paymentDataCreator);
        public async Task<string> CreateAndAuthorizePayment(PaymentInfoDTO info)
        {
            return "";
        }
    }
}
