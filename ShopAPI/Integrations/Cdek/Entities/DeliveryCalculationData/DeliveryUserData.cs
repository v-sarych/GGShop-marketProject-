using Integrations.Cdek.Entities.RegisterOrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek.Entities.DeliveryCalculationData
{
    internal class DeliveryUserData
    {
        public int? Tariff_code { get; set; }
        public FromLocation From_location { get; set; }
        public ToLocation To_location { get; set; }
    }
}
