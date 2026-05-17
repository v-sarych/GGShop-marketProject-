using Integrations.Cdek.Extentions;
using Integrations.YourPayments.Extentions;
using Microsoft.EntityFrameworkCore;
using ShopApiServer.Extentions;
using ShopDb;
using System.Reflection;
using ShopAPICore.Mapping;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/ShopApi-.log",
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
    builder.Services.AddDbContext<ShopDbContext>(options => 
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("ShopDb"));
        //options.UseSqlite("Data Source=Shop.db");
    });
    builder.Services.AddScoped<IShopDbContext>(provider => provider.GetService<ShopDbContext>());

    builder.Services.AddCastomAuthentication();

    builder.Services.AddRepositories();

    builder.Services.AddAutoMapper(cfg => { AutoMapperConfiguration.GetConfiguration(cfg); });

    builder.Services.AddControllersWithViews()
        .AddNewtonsoftJson(options => {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

    builder.Services.AddHttpClient();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(config =>
    {
        config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    });

    //Integratoins
    builder.Services.AddCdekIntegration();
    builder.Services.AddYourPaymentsIntegration();
}

void ConfigureApp(WebApplication app)
{
    
    //if (app.Environment.IsDevelopment())
    //{
        app.UseSwagger();
        app.UseSwaggerUI();
    //} - only for general development

    using(var scope = app.Services.CreateScope())
        using(var context = scope.ServiceProvider.GetService<ShopDbContext>())
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

    app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    app.UseCastomExeptionHandler();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseStaticFiles();

    app.MapControllers();
}
