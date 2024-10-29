using Integrations.YourPayments.Interfaces;
using Microsoft.Extensions.Logging;

namespace Integrations.YourPayments;

public class PaymentServiceUnitOfWork
{
    public readonly IHttpClientFactory HttpClientFactory;
    public readonly IPaymentDataCreator PaymentDataCreator;
    public readonly IPaymentRepository PaymentRepository;

    public readonly PaymentConfiguration Configuration;

    public readonly ILogger Logger;
    public PaymentServiceUnitOfWork(IHttpClientFactory httpClientFactory, IPaymentDataCreator paymentDataCreator,
        PaymentConfiguration paymentConfiguration, ILogger<YourPaymentIntegrationPaymentService> logger, IPaymentRepository paymentRepository)
        => (HttpClientFactory, PaymentDataCreator, Configuration, Logger, PaymentRepository) 
            = (httpClientFactory, paymentDataCreator, paymentConfiguration, logger, paymentRepository);
}