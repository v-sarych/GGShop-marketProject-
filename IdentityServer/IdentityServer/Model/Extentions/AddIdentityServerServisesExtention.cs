using IdentityServer.Model.Entities.Identity;
using IdentityServer.Model.interfaces;
using IdentityServer.Model.interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityServer.Model.Extentions
{
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
}
