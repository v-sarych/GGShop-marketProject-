using Integrations.Cdek.Entities.RegisterOrderEntities;
using Integrations.Cdek.Interfaces;
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
        private readonly IDeliveryDataFormatter _deliveryDataFormatter;
        private readonly IOAuthTokenFactory _tokenFactory;
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly CdekIntegrationConfiguration _configurration;

        private readonly ILogger _logger;
        public CdekIntegrationDeliveryService(IHttpClientFactory httpClientFactory, ILogger<CdekIntegrationDeliveryService> logger, IOAuthTokenFactory oAuthTokenFactory,
            IDeliveryDataFormatter deliveryDataFormatter, CdekIntegrationConfiguration cdekIntegrationConfiguration)
                => (_httpClientFactory, _logger, _tokenFactory, _deliveryDataFormatter, _configurration) =
                    (httpClientFactory, logger, oAuthTokenFactory, deliveryDataFormatter, cdekIntegrationConfiguration);
        public async Task TransferToDelivery(Guid orderId)
        {
            RegisterOrder order = await _deliveryDataFormatter.GetOrder(orderId);
            string bearerToken = "Bearer " + (await _tokenFactory.GetOAuthToken()).Access_token;

            HttpClient client = _httpClientFactory.CreateClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _configurration.RegisterOrderUrl);
            //request.Headers.Add("Content-Type", "application/json");
            request.Content = new StringContent(JsonSerializer.Serialize(order));
            request.Headers.Add("Authorization", bearerToken);

            HttpResponseMessage response =  await client.SendAsync(request);

            _logger.LogInformation("Register Order response", response.ToString());
        }
    }
}
