using Integrations.Cdek.Entities.OAuth;
using Integrations.Cdek.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek
{
    public class OAuthTokenFactory : IOAuthTokenFactory
    {
        private readonly IOAuthAuthorizationService _authorizationService;

        public readonly AuthorizeParametrs AuthorizeParametrs;
        public AccessObject AccessObject {  get; private set; }

        public OAuthTokenFactory(CdekIntegrationConfiguration cdekIntegrationConfiguration, IOAuthAuthorizationService oAuthAuthorizationService)
            => (_authorizationService, AuthorizeParametrs)  = (oAuthAuthorizationService, cdekIntegrationConfiguration.AuthorizeParametrs);
        public async Task<string> GetOAuthToken()
        {
            if (AccessObject == null)
                AccessObject = await _authorizationService.Authorizate(AuthorizeParametrs);

            return AccessObject.Access_token;
        }

        public async Task ReissueToken()
            => AccessObject = await _authorizationService.Authorizate(AuthorizeParametrs);
    }
}
