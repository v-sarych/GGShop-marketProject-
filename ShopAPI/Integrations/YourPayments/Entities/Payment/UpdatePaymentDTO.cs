using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Entities.Payment
{
    public class UpdatePaymentDTO
    {
        public Guid Id { get; set; }
        public string Status {  get; set; }
        public string AdditionalInfo {  get; set; }
    }
}
