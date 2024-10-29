using Integrations.YourPayments.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ShopAPICore.Interfaces.Services;

namespace Integrations.YourPayments.Extentions;

public static class AddYourPaymentsIntegrationExtention
{
    public static IServiceCollection AddYourPaymentsIntegration(this IServiceCollection services)
    {
        services.AddSingleton<PaymentConfiguration>(new PaymentConfiguration());

        services.AddScoped<IPaymentDataCreator, PaymentDataCreator>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<PaymentServiceUnitOfWork>();
        services.AddScoped<IPaymentService, YourPaymentIntegrationPaymentService>();

        return services;
    }
}