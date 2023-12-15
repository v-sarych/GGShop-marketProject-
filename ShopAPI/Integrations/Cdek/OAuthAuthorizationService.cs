using Integrations.Cdek.Entities.OAuth;
using Integrations.Cdek.Interfaces;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Integrations.Cdek
{
    public class OAuthAuthorizationService : IOAuthAuthorizationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OAuthAuthorizationService(IHttpClientFactory httpClientFactory) 
            => _httpClientFactory = httpClientFactory;
        public async Task<AccessObject> Authorizate(AuthorizeParametrs parametrs)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpContent httpContent = null;

            switch (parametrs.ContentType)
            {
                case "x-www-form-urlencoded":
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { nameof(parametrs.Client_id).ToLower(), parametrs.Client_id },
                        { nameof(parametrs.Client_secret).ToLower(), parametrs.Client_secret },
                        { nameof(parametrs.Grant_type).ToLower(), parametrs.Grant_type }
                    };

                    httpContent = new FormUrlEncodedContent(data);
                    break;

                default:
                    throw new Exception("Incorrect ContentType in configuration");
            }

            HttpResponseMessage response = await httpClient.PostAsync(parametrs.Url, httpContent);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new Exception("invalid account data");
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("response status: " + Convert.ToString(response.StatusCode));

            string responseContentText = await response.Content.ReadAsStringAsync();
            var responseDeserializeOptions = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<AccessObject>(responseContentText, responseDeserializeOptions);
        }
    }
}
