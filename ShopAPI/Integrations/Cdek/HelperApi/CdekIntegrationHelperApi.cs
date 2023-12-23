﻿using Integrations.Cdek.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace Integrations.Cdek.HelperApi
{
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
        public async Task GetDataForWidget([Required] Guid orderId)
            => await _dataCreator.GetDataForWidget(orderId);
    }
}
