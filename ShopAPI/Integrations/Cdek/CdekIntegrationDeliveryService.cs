using Integrations.Cdek.Entities.RegisterOrderEntities;
using Integrations.Cdek.Interfaces;
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
        public CdekIntegrationDeliveryService(IHttpClientFactory httpClientFactory,IOAuthTokenFactory oAuthTokenFactory,
            IDeliveryDataFormatter deliveryDataFormatter, CdekIntegrationConfiguration cdekIntegrationConfiguration)
                => (_httpClientFactory, _tokenFactory, _deliveryDataFormatter, _configurration) =
                    (httpClientFactory, oAuthTokenFactory, deliveryDataFormatter, cdekIntegrationConfiguration);
        public async Task TransferToDelivery(Guid orderId)
        {
            RegisterOrder order = await _deliveryDataFormatter.GetOrder(orderId);
            string bearerToken = "Bearer " + (await _tokenFactory.GetOAuthToken()).Access_token;

            HttpClient client = _httpClientFactory.CreateClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _configurration.RegisterOrderUrl);
            request.Content = new StringContent(JsonSerializer.Serialize<RegisterOrder>(order), 
                System.Text.Encoding.UTF8, "application/json");
            request.Headers.Add("Authorization", bearerToken);

            HttpResponseMessage response =  await client.SendAsync(request);

        }
    }
}
