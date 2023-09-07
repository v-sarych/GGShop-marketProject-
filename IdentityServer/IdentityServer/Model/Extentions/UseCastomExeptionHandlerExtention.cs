using ShopApi.Middelware;
using System.Runtime.CompilerServices;

namespace ShopApi.Extentions
{
    internal static class UseCastomExeptionHandlerExtention
    {
        internal static IApplicationBuilder UseCastomExeptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CastomExeptionHandlerMiddelware>();

            return builder;
        }
    }
}
