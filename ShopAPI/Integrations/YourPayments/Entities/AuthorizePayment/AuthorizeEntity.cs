using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Entities.AuthorizePayment
{
    internal class AuthorizeEntity
    {
        public string PaymentMethod { get; set; }
        public bool UsePaymentPage { get; set; }
    }
}
