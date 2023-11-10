using Integrations.Cdek.Interfaces;
using Integrations.Cdek.Entities.OAuth;
using Microsoft.Extensions.DependencyInjection;
using ShopApiCore.Interfaces.Services;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Integrations.Cdek.Extentions
{
    public static class CdekIntegrationExtentions
    {
        public static IServiceCollection AddCdekIntegration(this IServiceCollection services)
        {
            CdekIntegrationConfiguration cdekConfiguration = new CdekIntegrationConfiguration(new AuthorizeParametrs()
            {
                Client_id = "EMscd6r9JnFiQ3bLoyjJY6eM78JrJceI",
                Client_secret = "PjLZkKBHEiLK3YsjtNrt3TGNG0ahs3kG",
                Grant_type = "client_credentials",
                ContentType = "x-www-form-urlencoded",
                Url = "https://api.edu.cdek.ru/v2/oauth/token"
            });
            services.AddSingleton<CdekIntegrationConfiguration>(new CdekIntegrationConfiguration(new AuthorizeParametrs()));

            services.AddSingleton<IOAuthAuthorizationService, OAuthAuthorizationService>();
            services.AddSingleton<IOAuthTokenFactory, OAuthTokenFactory>();

            services.AddScoped<IDeliveryService, CdekIntegrationDeliveryService>();
            services.AddScoped<IDeliveryDataFormatter, DeliveryDataFormatter>();

            Console.WriteLine("Cdek added successfuly");

            return services;
        }
    }
}
