using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Identity.Core.Model;

namespace IdentityServer.Model.Extentions
{
    public static class AddCastomAuthenticationExtention
    {
        public static IServiceCollection AddCastomAuthentication(this IServiceCollection services)
        {
            JwtConfiguration jwtConfiguration = new JwtConfiguration();
            services.AddSingleton<JwtConfiguration>(jwtConfiguration);

            services.AddAuthentication("JWT")
                .AddJwtBearer("JWT", options => {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfiguration.Issuer,
                        ValidateAudience = false,

                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SymetricKey))
                    };
                });

            return services;
        }
    }
}
