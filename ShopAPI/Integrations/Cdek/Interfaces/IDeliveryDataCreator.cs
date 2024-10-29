using Integrations.Cdek.Entities.DeliveryCalculationData;
using Integrations.Cdek.Entities.RegisterOrderEntities;
using ShopAPICore.Entities.DTO.OrderItem;

namespace Integrations.Cdek.Interfaces;

public interface IDeliveryDataCreator
{
    Task<RegisterOrder> CreateRegisterOrderObject(Guid id);
    Task<DeliveryWidgetCalculationDataDTO> GetDataForWidget(ICollection<CreateOrderItemDTO> orderItems);
}