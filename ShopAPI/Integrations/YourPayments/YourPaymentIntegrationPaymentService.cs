﻿using ShopApi.Model.Entities.DTO.Payments;
using ShopApi.Model.Interfaces.Services;

namespace Integrations.YourPayments
{
    internal class YourPaymentIntegrationPaymentService : IPaymentService
    {
        public Task<string> CreateAndAuthorizePayment(PaymentInfoDTO info)
        {
            throw new NotImplementedException();
        }
    }
}