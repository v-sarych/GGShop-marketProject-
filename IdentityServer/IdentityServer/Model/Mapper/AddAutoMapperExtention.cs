using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;

namespace IdentityServer.Model.Mapper
{
    public static class AddMapperExtention
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
