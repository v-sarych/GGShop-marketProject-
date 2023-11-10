using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace Integrations.Cdek.HelperApi
{
    [Route("api/CdekIntegrationHelper")]
    [ApiController]
    public class CdekIntegrationHelperApi : ControllerBase
    {

        [HttpPost("WebHooks/UpdateOrderStatus")]
        public async Task UpdateOrderStatusWebHook([Required] Guid orderId, [Required] string key)
        {

        }
    }
}
