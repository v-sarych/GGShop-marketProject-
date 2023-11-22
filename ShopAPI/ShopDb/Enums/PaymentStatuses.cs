using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDb.Enums
{
    public class PaymentStatuses
    {
        public const string Success = "Success";
        public const string WaitGateway = "WaitGateway";
        public const string Failed = "Failed";
    }
}
