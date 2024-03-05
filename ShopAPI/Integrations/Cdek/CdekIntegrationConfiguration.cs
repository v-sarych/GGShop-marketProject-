using Integrations.Cdek.Entities.OAuth;
using Integrations.Cdek.Entities.OAuth;
using Integrations.Cdek.Entities.OAuth;
using Integrations.Cdek.Entities.RegisterOrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek
{
    public class CdekIntegrationConfiguration
    {
        public readonly AuthorizeParametrs AuthorizeParametrs;

        public readonly string RegisterOrderUrl = "https://api.edu.cdek.ru/v2/orders";
        public readonly string CalculateDeliveryUlr = "https://api.edu.cdek.ru/v2/calculator/tariff";

        public FromLocation[] From_locations { get; set; }

        public CdekIntegrationConfiguration(AuthorizeParametrs authorizeParametrs)
        {
            AuthorizeParametrs = authorizeParametrs;
        }
    }
}
