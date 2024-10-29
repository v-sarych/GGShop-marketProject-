namespace ShopAPICore.Entities.DTO.Payments;

public class PaymentInfoDTO
{
    public Guid OrderId { get; set; }

    public string PaymentMethod { get; set; }
    public PaymentClientDTO? paymentClientDTO {  get; set; }
    public string Currency {  get; set; }
}