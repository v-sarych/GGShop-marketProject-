using FileServer.Middelware;
using System.Runtime.CompilerServices;

namespace FileServer.Model.Extentions
{
    public static class UseCastomExeptionHandlerExtention
    {
        public static IApplicationBuilder UseCastomExeptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CastomExeptionHandlerMiddelware>();

            return builder;
        }
    }
}
