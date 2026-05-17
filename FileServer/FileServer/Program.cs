using FileServer.Model.Extentions;
using IdentityServer.Model.Extentions;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/FileServer-.log",
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10_485_760,      // 10 МБ
        retainedFileCountLimit: 7,           // Хранить 7 последних файлов
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
.CreateLogger();

builder.Host.UseSerilog();

ConfigureServices(builder);

var app = builder.Build();

ConfigureApp(app);

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();

    builder.Services.AddCastomAuthentication();

    builder.Services.AddFileComponents(builder.Configuration, builder.Environment);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

void ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    app.UseCastomExeptionHandler();

    app.UseCustomizedStaticFiles();

    app.MapControllerRoute(
                name: "default",
                pattern: "{action=UserView}",
                defaults: new { controller = "View", action = "UserView" });

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}
