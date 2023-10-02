using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments
{
    internal class PaymentConfiguration
    {
        public readonly string MerchantId = "merchant";
        public readonly string Signature = "key";

        public PaymentConfiguration() { }
    }
}
