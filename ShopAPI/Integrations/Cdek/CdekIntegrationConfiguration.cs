using Integrations.Cdek.Entities.OAuth;
using Integrations.Cdek.Entities.OAuth;
using Integrations.Cdek.Entities.OAuth;
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

        public CdekIntegrationConfiguration(AuthorizeParametrs authorizeParametrs)
        {
            AuthorizeParametrs = authorizeParametrs;
        }
    }
}
