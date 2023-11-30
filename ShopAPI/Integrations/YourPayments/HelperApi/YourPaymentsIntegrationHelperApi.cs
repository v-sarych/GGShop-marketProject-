using Integrations.YourPayments.Entities.Payment;
using Integrations.YourPayments.Entities.PaymentWebHookData;
using Integrations.YourPayments.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ShopDb.Enums;
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
        private readonly PaymentConfiguration _configuration;
        private readonly IPaymentRepository _repository;

        public YourPaymentsIntegrationHelperApi(PaymentConfiguration configuration, IPaymentRepository repository)
            => (_configuration, _repository) = (configuration, repository);

        [HttpPost("WebHooks/PaymentData")]
        public async Task PaymentDataWebHook([FromBody]WebHookDataDTO dto, [FromQuery]string webHookKey)
        {
            if (webHookKey != _configuration.WebHookKey)
                return;

            UpdatePaymentDTO paymentDTO = new UpdatePaymentDTO()
            {
                Id = Guid.Parse(dto.OrderData.MerchantPaymentReference),
                IdInGateway = Guid.Parse(dto.OrderData.PayuPaymentReference),
                AdditionalInfo = HttpContext.Request.Body.ToString()
            };

            if (dto.OrderData.Status == "COMPLETE")
                paymentDTO.Status = PaymentStatuses.Success;
            else if (dto.OrderData.Status == "INVALID")
                paymentDTO.Status = PaymentStatuses.Failed;
            else
                paymentDTO.Status = PaymentStatuses.WaitGateway;

            await _repository.UpdatePaymnentData(paymentDTO);
        }
    }
}
