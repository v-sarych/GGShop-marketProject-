using IdentityServer.Model.Extentions;
using Integrations.Cdek.Extentions;
using Microsoft.EntityFrameworkCore;
using ShopApiCore.Mapping;
using ShopApiServer.Extentions;
using ShopDb;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

ConfigureApp(app);

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ShopDbContext>(options => 
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("ShopDb"));
        //options.UseSqlite("Data Source=Shop.db");
    });
    builder.Services.AddScoped<IShopDbContext>(provider => provider.GetService<ShopDbContext>());

    builder.Services.AddCastomAuthentication();

    builder.Services.AddRepositories();
    builder.Services.AddServices();

    builder.Services.AddAutoMapper(cfg => { AutoMapperConfiguration.GetConfiguration(cfg); });

    builder.Services.AddControllersWithViews()
        .AddNewtonsoftJson(options => {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

    builder.Services.AddHttpClient();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Integratoins
    builder.Services.AddCdekIntegration();
}

void ConfigureApp(WebApplication app)
{
    
    //if (app.Environment.IsDevelopment())
    //{
        app.UseSwagger();
        app.UseSwaggerUI();
    //} - only for general development

    /*using(var scope = app.Services.CreateScope())
        using(var context = scope.ServiceProvider.GetService<ShopDbContext>())
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }*/

    app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    app.UseCastomExeptionHandler();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseStaticFiles();

    app.MapControllers();
}
