using Integrations.Cdek.Entities.DeliveryCalculationData;
using Integrations.Cdek.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ShopAPICore.Entities.DTO.OrderItem;

namespace Integrations.Cdek.HelperApi;

[Route("api/CdekIntegration")]
[ApiController]
public class CdekIntegrationHelperApi : ControllerBase
{
    private readonly IDeliveryDataCreator _dataCreator;
    public CdekIntegrationHelperApi(IDeliveryDataCreator dataCreator)
        => _dataCreator = dataCreator;

    [HttpPost("WebHooks/UpdateOrderStatus")]
    public async Task UpdateOrderStatusWebHook([Required] Guid orderId, [Required] string key)
    {

    }

    [HttpPost("GetDataForWidget")]
    public async Task<DeliveryWidgetCalculationDataDTO> GetDataForWidget([Required][FromBody] ICollection<CreateOrderItemDTO> orderItems)
        => await _dataCreator.GetDataForWidget(orderItems);
}