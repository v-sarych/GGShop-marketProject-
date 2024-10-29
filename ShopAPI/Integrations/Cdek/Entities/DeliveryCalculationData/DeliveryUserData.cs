using Integrations.Cdek.Entities.RegisterOrderEntities;

namespace Integrations.Cdek.Entities.DeliveryCalculationData;

internal class DeliveryUserData
{
    public int? Tariff_code { get; set; }
    public FromLocation From_location { get; set; }
    public ToLocation To_location { get; set; }
}