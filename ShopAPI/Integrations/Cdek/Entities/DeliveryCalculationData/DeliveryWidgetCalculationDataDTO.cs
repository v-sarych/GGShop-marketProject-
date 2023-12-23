using Integrations.Cdek.Entities.RegisterOrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek.Entities.DeliveryCalculationData
{
    public class DeliveryWidgetCalculationDataDTO
    {
        public FromLocation From_location { get; set; }
        public ToLocation To_location { get; set; }

        public Package[] Packages { get; set; }
    }
}
