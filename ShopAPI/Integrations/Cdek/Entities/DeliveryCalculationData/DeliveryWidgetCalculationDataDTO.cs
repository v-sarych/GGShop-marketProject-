using Integrations.Cdek.Entities.RegisterOrderEntities;

namespace Integrations.Cdek.Entities.DeliveryCalculationData;

public class DeliveryWidgetCalculationDataDTO
{
    public FromLocation[] From_locations { get; set; }

    public Package[] Packages { get; set; }
}