﻿using Integrations.YourPayments.Entities;
using Integrations.YourPayments.Entities.AuthorizePayment;
using ShopApiCore.Entities.DTO.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Interfaces
{
    internal interface IPaymentDataCreator
    {
        Task<AuthorizePaymentData> GetAuthorizePaymentData(PaymentInfoDTO dto);
        Task<string> GetSignature(SignatureParameters parameters);
        Task<string> GetMerchant();
    }
}
