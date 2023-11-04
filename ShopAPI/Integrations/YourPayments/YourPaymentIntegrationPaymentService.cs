using Integrations.YourPayments.Entities.AuthorizePayment;
using Integrations.YourPayments.Interfaces;
using Microsoft.Extensions.Logging;
using ShopApiCore.Entities.DTO.Payments;
using ShopApiCore.Interfaces.Services;
using System.Net.Http.Json;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace Integrations.YourPayments
{
    public class YourPaymentIntegrationPaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPaymentDataCreator _paymentDataCreator;
        
        private readonly ILogger _logger;
        public YourPaymentIntegrationPaymentService(IHttpClientFactory httpClientFactory, IPaymentDataCreator paymentDataCreator, ILogger<YourPaymentIntegrationPaymentService> logger)
            => (_httpClientFactory, _paymentDataCreator, _logger) = (httpClientFactory, paymentDataCreator, logger);
        public async Task<string> CreateAndAuthorizePayment(PaymentInfoDTO info)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(await _createMessage(info));

            _logger.LogInformation(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpRequestMessage> _createMessage(PaymentInfoDTO info)
        {
            AuthorizePaymentData authorizeData = await _paymentDataCreator.GetAuthorizePaymentData(info);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "");
            message.Content = new StringContent(JsonSerializer.Serialize(authorizeData));

            string merchant = await _paymentDataCreator.GetMerchant();
            string date = DateTime.Now.ToString();

            message.Headers.Add("X-Header-Merchant", merchant);
            message.Headers.Add("X-Header-Date", date);

            string mD5BodySum = "";
            message.Headers.Add("X-Header-Signature", await _paymentDataCreator.GetSignature(new Entities.SignatureParameters()
            {
                BaseUrl = "",
                Date = date,
                HttpMethod = "POST",
                Merchant = merchant,
                MD5BodySum = mD5BodySum
            }));
            return message;
        }
    }
}
