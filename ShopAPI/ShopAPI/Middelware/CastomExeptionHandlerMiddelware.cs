using ShopApiServer.Extentions;
using ShopApiCore.Exceptions;
using System.Net;
using System.Text.Json;

namespace ShopApiServer.Middelware
{
    public class CastomExeptionHandlerMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public CastomExeptionHandlerMiddelware(RequestDelegate next, ILogger<CastomExeptionHandlerMiddelware> logger)
            => (_next, _logger) = (next, logger);

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                await _handleExeption(context, ex);
            }
        }

        private async Task _handleExeption(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case NotFoundException:
                    context.Response.StatusCode = 404;
                    break;

                case NoPermissionsException:
                    context.Response.StatusCode = 403;
                    break;

                case AlreadyExistException:
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("AlreadyExist");
                    break;

                case NotInStockException:
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("NotInStock");
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(exception.Message));
                    break;
            }
        }
    }
}
