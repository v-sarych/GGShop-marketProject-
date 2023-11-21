using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.HelperApi
{
    [Route("api/YourPaymentsIntegrationHelper")]
    [ApiController]
    public class YourPaymentsIntegrationHelperApi : ControllerBase
    {
        [HttpPost("WebHooks/Payment")]
        public async Task PaymentWebHook()
        {

        }
    }
}
