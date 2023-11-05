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

        private readonly PaymentConfiguration _configuration;
        
        private readonly ILogger _logger;
        public YourPaymentIntegrationPaymentService(IHttpClientFactory httpClientFactory, IPaymentDataCreator paymentDataCreator,
            PaymentConfiguration paymentConfiguration, ILogger<YourPaymentIntegrationPaymentService> logger)
            => (_httpClientFactory, _paymentDataCreator, _configuration, _logger) = (httpClientFactory, paymentDataCreator, paymentConfiguration, logger);
        public async Task<string> CreateAndAuthorizePayment(PaymentInfoDTO info)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(await _createAuthorizeMessage(info));

            _logger.LogInformation(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpRequestMessage> _createAuthorizeMessage(PaymentInfoDTO info)
        {
            AuthorizePaymentData authorizeData = await _paymentDataCreator.GetAuthorizePaymentData(info);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, _configuration.AuthorizePaymentUrl);

            string contentString = JsonSerializer.Serialize(authorizeData);
            message.Content = new StringContent(contentString);

            string merchant = _configuration.MerchantId;
            string date = DateTime.Now.ToString();

            message.Headers.Add("X-Header-Merchant", merchant);
            message.Headers.Add("X-Header-Date", date);

            string mD5BodySum = await _paymentDataCreator.GetMD5(contentString);
            message.Headers.Add("X-Header-Signature", await _paymentDataCreator.GetSignature(new Entities.SignatureParameters()
            {
                BaseUrl = _configuration.AuthorizePaymentUrl,
                Date = date,
                HttpMethod = "POST",
                Merchant = merchant,
                MD5BodySum = mD5BodySum
            }));
            return message;
        }
    }
}
