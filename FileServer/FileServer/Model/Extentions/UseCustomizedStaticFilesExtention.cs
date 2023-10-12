using Microsoft.Extensions.FileProviders;
using System.Runtime.CompilerServices;

namespace FileServer.Model.Extentions
{
    public static class UseCustomizedStaticFilesExtention
    {
        public static WebApplication UseCustomizedStaticFiles(this WebApplication app)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(app.Environment.WebRootPath, "Views"))
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/api/File"
            });

            return app;
        }
    }
}
