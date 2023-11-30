using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Entities.PaymentWebHookData
{
    public class OrderData
    {
        public string OrderDate {  get; set; }
        public string PayuPaymentReference { get; set; }
        public string MerchantPaymentReference { get; set; }
        public string Status {  get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
    }
}
