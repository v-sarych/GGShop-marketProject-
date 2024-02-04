using ShopDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Entities.Payment
{
    public class PaymentDTO
    {
        public string AdditionalDetails { get; set; }
        public Guid OrderId { get; set; }
    }
}
