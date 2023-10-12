﻿using ShopDb.Entities;

namespace ShopApi.Model.Entities.DTO.Payments
{
    public class PaymentInfoDTO
    {
        public Guid OrderId { get; set; }

        public PaymentClientDTO paymentClientDTO {  get; set; }
        public string Currency {  get; set; }
    }
}