using System.Text.Json;

namespace ShopApiServer.Extentions
{
    public static class ConfigureJsonOptionsExtention
    {
        public static void ConfigureJsonOptions(this WebApplicationBuilder builder)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
    }
}
