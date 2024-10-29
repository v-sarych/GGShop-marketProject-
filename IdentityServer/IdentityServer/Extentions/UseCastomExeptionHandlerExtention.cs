using ShopApi.Middelware;

namespace ShopApiServer.Extentions;

internal static class UseCastomExeptionHandlerExtention
{
    internal static IApplicationBuilder UseCastomExeptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<CastomExeptionHandlerMiddelware>();

        return builder;
    }
}