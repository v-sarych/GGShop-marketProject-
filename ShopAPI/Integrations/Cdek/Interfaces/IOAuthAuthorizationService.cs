using Integrations.Cdek.Entities.OAuth;

namespace Integrations.Cdek.Interfaces;

public interface IOAuthAuthorizationService
{
    Task<AccessObject> Authorizate(AuthorizeParametrs parametrs);
}