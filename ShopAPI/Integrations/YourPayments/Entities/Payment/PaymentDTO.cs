namespace Integrations.YourPayments.Entities.Payment;

public class PaymentDTO
{
    public string AdditionalDetails { get; set; }
    public Guid OrderId { get; set; }
}