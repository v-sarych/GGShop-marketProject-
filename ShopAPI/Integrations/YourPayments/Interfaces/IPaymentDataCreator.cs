﻿using Integrations.YourPayments.Entities;
using Integrations.YourPayments.Entities.AuthorizePayment;
using ShopAPICore.Entities.DTO.Payments;

namespace Integrations.YourPayments.Interfaces;

public interface IPaymentDataCreator
{
    Task<AuthorizePaymentData> GetAuthorizePaymentData(PaymentInfoDTO dto);
    Task<string> GetSignature(SignatureParameters parameters);
    Task<string> GetMD5(byte[] data);
}