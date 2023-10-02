using Integrations.YourPayments.Entities;
using Integrations.YourPayments.Interfaces;
using ShopApi.Model.Entities.DTO.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments
{
    internal class PaymentDataCreator : IPaymentDataCreator
    {

        public async Task<AuthorizePaymentData> FromDTO(PaymentInfoDTO dto, PaymentConfiguration configuration)
        {
            AuthorizePaymentData result = new AuthorizePaymentData();

            return result;
        }
    }
}
