using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments
{
    public class PaymentConfiguration
    {
        public readonly string MerchantId = "sidyfwuf";
        public readonly string SecretKey = "RL(GS1^5(5s3)l8b3&9&";

        public readonly string WebHookKey = ";jflslkjflksdnglksdnvdnglsdkfnlsdfnlsldkfshg;l;gm'slgjS";

        public readonly string GatewayHost = "https://sandbox.ypmn.ru";
        public readonly string AuthorizePaymentUrl = "/api/v4/payments/authorize";

        public PaymentConfiguration() { }
    }
}
