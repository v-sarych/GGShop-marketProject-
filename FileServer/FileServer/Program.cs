using FileServer.Model.Extentions;
using IdentityServer.Model.Extentions;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

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
