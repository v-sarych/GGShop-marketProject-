using Integrations.Cdek.Entities.OAuth;

namespace Integrations.Cdek.Interfaces;

public interface IOAuthTokenFactory
{
    Task<AccessObject> GetOAuthToken();
    Task ReissueToken();
}