using Integrations.YourPayments.Entities;
using ShopApi.Model.Entities.DTO.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Interfaces
{
    internal interface IPaymentDataCreator
    {
        Task<AuthorizePaymentData> FromDTO(PaymentInfoDTO dto, PaymentConfiguration configuration);
    }
}
