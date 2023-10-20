using Integrations.Cdek.Entities.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek.Interfaces
{
    public interface IOAuthAuthorizationService
    {
        Task<AccessObject> Authorizate(AuthorizeParametrs parametrs);
    }
}
