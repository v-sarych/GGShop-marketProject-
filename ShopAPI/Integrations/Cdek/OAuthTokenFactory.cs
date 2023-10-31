using Integrations.Cdek.Entities.OAuth;
using Integrations.Cdek.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OAuthTokenFactory> _logger;

        public readonly AuthorizeParametrs AuthorizeParametrs;
        private AccessObject _accessObject {  get; set; }

        public OAuthTokenFactory(ILogger<OAuthTokenFactory> logger, CdekIntegrationConfiguration cdekIntegrationConfiguration, IOAuthAuthorizationService oAuthAuthorizationService)
            => (_logger, _authorizationService, AuthorizeParametrs)  = (logger, oAuthAuthorizationService, cdekIntegrationConfiguration.AuthorizeParametrs);
        public async Task<AccessObject> GetOAuthToken()
        {
            if (_accessObject == null)
            {
                _accessObject = await _authorizationService.Authorizate(AuthorizeParametrs);
                _logger.LogInformation("Cdek oauth token is reissue");
            }

            return _accessObject;
        }

        public async Task ReissueToken()
        {
            _accessObject = await _authorizationService.Authorizate(AuthorizeParametrs);

            _logger.LogInformation("Cdek oauth token is reissue");
        }
    }
}
