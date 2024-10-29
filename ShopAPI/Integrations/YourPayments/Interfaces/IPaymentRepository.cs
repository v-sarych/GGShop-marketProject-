using Integrations.YourPayments.Entities.Payment;

namespace Integrations.YourPayments.Interfaces;

public interface IPaymentRepository
{
    Task<bool> CanCreatePayment(Guid id);
    Task<string> GetPaymentStatus(Guid id);
    Task CreatePayment(PaymentDTO payment);
    Task UpdatePaymnentData(UpdatePaymentDTO info);
}