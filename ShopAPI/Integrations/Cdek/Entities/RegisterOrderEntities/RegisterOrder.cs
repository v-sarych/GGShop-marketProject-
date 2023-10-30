namespace Integrations.Cdek.Entities.RegisterOrderEntities
{
    public class RegisterOrder
    {
        public Guid Number { get; set; }
        public int Tariff_code { get; set; }

        public Recipient Recipient { get; set; }

        public string? Shipment_point { get; set; }
        public string? Delivery_point { get; set; }

        public FromLocation? From_location { get; set; }
        public ToLocation? To_location { get; set; }

        public DeliveryRecipientCost? Delivery_recipient_cost {  get; set; }
        public DeliveryRecipientCostAdv[]? Delivery_recipient_cost_adv {  get; set; }

        public Package[] Packages { get; set; }
    }
}