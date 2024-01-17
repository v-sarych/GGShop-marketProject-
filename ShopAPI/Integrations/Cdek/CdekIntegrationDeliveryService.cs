using Integrations.Cdek.Entities.RegisterOrderEntities;
using Integrations.Cdek.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using ShopApiCore.Interfaces.Services;
using ShopDb;
using ShopDb.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Integrations.Cdek
{
    public class CdekIntegrationDeliveryService : IDeliveryService
    {
        private readonly IDeliveryDataCreator _deliveryDataFormatter;
        private readonly IOAuthTokenFactory _tokenFactory;
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly CdekIntegrationConfiguration _configurration;

        private readonly ILogger _logger;
        public CdekIntegrationDeliveryService(IHttpClientFactory httpClientFactory, ILogger<CdekIntegrationDeliveryService> logger, IOAuthTokenFactory oAuthTokenFactory,
            IDeliveryDataCreator deliveryDataFormatter, CdekIntegrationConfiguration cdekIntegrationConfiguration)
                => (_httpClientFactory, _logger, _tokenFactory, _deliveryDataFormatter, _configurration) =
                    (httpClientFactory, logger, oAuthTokenFactory, deliveryDataFormatter, cdekIntegrationConfiguration);

        public async Task<float> CalculateDeliveryCost(string deliveryInfo)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            string bearerToken = "Bearer " + (await _tokenFactory.GetOAuthToken()).Access_token;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _configurration.RegisterOrderUrl);
            request.Content = new StringContent(deliveryInfo, System.Text.Encoding.UTF8, "application/json");
            request.Headers.Add("Authorization", bearerToken);

            HttpResponseMessage response = await client.SendAsync(request);

            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                _logger.LogInformation("CalculatingDeliveryCost is success. Response - " + responseContent);
            else
                throw new Exception("CalculatingDeliveryCost is not success. Response - " + responseContent);
            
            return JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement.GetProperty("total_sum").GetSingle();
        }

        public async Task TransferToDelivery(Guid orderId)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            RegisterOrder order = await _deliveryDataFormatter.CreateRegisterOrderObject(orderId);
            string bearerToken = "Bearer " + (await _tokenFactory.GetOAuthToken()).Access_token;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _configurration.RegisterOrderUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(order),  System.Text.Encoding.UTF8, "application/json");
            request.Headers.Add("Authorization", bearerToken);

            HttpResponseMessage response =  await client.SendAsync(request);

            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                _logger.LogInformation("TransferToDelivery is success. Response - " + responseContent);
            else
                throw new Exception("TransferToDelivery is not success. Response - " + responseContent);
        }
    }
}
