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
        private AccessObject _accessObject {  get; set; }

        public OAuthTokenFactory(CdekIntegrationConfiguration cdekIntegrationConfiguration, IOAuthAuthorizationService oAuthAuthorizationService)
            => (_authorizationService, AuthorizeParametrs)  = (oAuthAuthorizationService, cdekIntegrationConfiguration.AuthorizeParametrs);
        public async Task<AccessObject> GetOAuthToken()
        {
            if (_accessObject == null)
                _accessObject = await _authorizationService.Authorizate(AuthorizeParametrs);

            return _accessObject;
        }

        public async Task ReissueToken()
            => _accessObject = await _authorizationService.Authorizate(AuthorizeParametrs);
    }
}
