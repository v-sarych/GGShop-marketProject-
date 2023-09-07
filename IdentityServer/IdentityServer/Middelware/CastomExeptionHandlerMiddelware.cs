using System.Net;
using System.Text.Json;

namespace ShopApi.Middelware
{
    public class CastomExeptionHandlerMiddelware
    {
        private readonly RequestDelegate _next;
        public CastomExeptionHandlerMiddelware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await _handleExeption(context, ex);
            }
        }

        private async Task _handleExeption(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(exception.Message));
                    break;
            }
        }
    }
}
