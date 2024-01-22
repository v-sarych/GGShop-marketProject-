using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Entities.AuthorizePayment
{
    public class AuthorizePaymentData
    {
        public string MerchantPaymentReference { get; set; }
        public string ReturnUrl { get; set; }
        public string Currency { get; set; }

        public AuthorizeEntity Authorization { get; set; }
        public ClientEntity Client { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
    }
}
