﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Entities.AuthorizePayment
{
    public class AuthorizeEntity
    {
        public string PaymentMethod { get; set; }
        public string UsePaymentPage { get; set; }
    }
}
