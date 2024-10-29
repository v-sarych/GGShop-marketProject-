using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ShopApiServer.Extentions;

public static class AddCastomAuthenticationExtention
{
    public static IServiceCollection AddCastomAuthentication(this IServiceCollection services)
    {
        var jwtConfiguration = new JwtConfiguration();
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