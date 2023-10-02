using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Entities
{
    internal class ProductEntity
    {
        public string Name {  get; set; }
        public string Sku {  get; set; }
        public string UnitPrice { get; set; }
        public UInt16 Quantity {  get; set; }
    }
}
