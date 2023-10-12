using ShopApi.Model.Interfaces.Services;
using Integrations.YourPayments;
using Integrations.Cdek;

namespace ShopApi.Extentions
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
