using ShopApiCore.Interfaces.Services;
using Integrations.YourPayments;
using Integrations.Cdek;

namespace ShopApiServer.Extentions
{
    internal static class AddServicesExtention
    {
        internal static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, YourPaymentIntegrationPaymentService>();
            services.AddScoped<IDeliveryService, CdekIntegrationDeliveryService>();

            return services;
        }
    }
}
