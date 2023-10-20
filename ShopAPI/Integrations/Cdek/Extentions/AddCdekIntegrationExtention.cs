using Integrations.Cdek.Interfaces;
using Integrations.Cdek.Entities.OAuth;
using Microsoft.Extensions.DependencyInjection;
using ShopApiCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek.Extentions
{
    public static class AddCdekIntegrationExtention
    {
        public static IServiceCollection AddCdekIntegration(this IServiceCollection services)
        {
            services.AddScoped<IDeliveryService, CdekIntegrationDeliveryService>();
            services.AddScoped<IOAuthAuthorizationService, OAuthAuthorizationService>();
            services.AddSingleton<CdekIntegrationConfiguration>(_cdekConfiguration_init());

            return services;
        }

        private static CdekIntegrationConfiguration _cdekConfiguration_init()
        {
            CdekIntegrationConfiguration cdekConfiguration = new CdekIntegrationConfiguration(new AuthorizeParametrs()
            {

            });

            return cdekConfiguration;
        }
    }
}
