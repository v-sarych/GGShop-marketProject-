using ShopDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Entities
{
    public class PaymentDTO
    {
        public Guid? IdInPaymentGateway { get; set; }
        public string AdditionalDetails { get; set; }
        public Guid OrderId { get; set; }
    }
}
