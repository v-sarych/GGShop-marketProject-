using Integrations.YourPayments.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ShopApiCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Extentions
{
    public static class AddYourPaymentsIntegrationExtention
    {
        public static IServiceCollection AddYourPaymentsIntegration(this IServiceCollection services)
        {
            services.AddSingleton<PaymentConfiguration>(new PaymentConfiguration());

            services.AddScoped<IPaymentDataCreator, PaymentDataCreator>();
            services.AddScoped<IPaymentService, YourPaymentIntegrationPaymentService>();

            return services;
        }
    }
}
