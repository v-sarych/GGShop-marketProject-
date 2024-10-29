using Identity.Core.Model.interfaces;
using Identity.Core.Model.interfaces.Repositories;

namespace ShopApiServer.Extentions;

internal static class AddIdentityServerServisesExtention
{
    internal static IServiceCollection AddIdentityServerServises(this IServiceCollection services)
    {
        services.AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<IJwtCreator, JwtCreator>()
            .AddTransient<ISessionRepository, SessionRepository>();
            
        return services;
    }
}