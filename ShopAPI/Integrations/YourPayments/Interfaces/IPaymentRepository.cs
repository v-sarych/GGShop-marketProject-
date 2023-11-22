﻿using Integrations.YourPayments.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Interfaces
{
    public interface IPaymentRepository
    {
        Task<bool> CanCreatePayment(Guid id);
        Task CreatePayment(PaymentDTO payment);
    }
}
