using Identity.Core.Model;
using Identity.Core.Model.interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Model.Mapper;
using ShopApiServer.Extentions;
using ShopDb;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/IdentityServer-.log",
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10_485_760,      // 10 МБ
        retainedFileCountLimit: 7,           // Хранить 7 последних файлов
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
.CreateLogger();

builder.Host.UseSerilog();

ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseCastomExeptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{

    builder.Services.AddTransient<IUserRepository, UserRepository>();

    builder.Services.AddDbContext<ShopDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("ShopDb"));
        //options.UseSqlite($"Data Source={builder.Environment.}\\ShopAPI\\Shop.db");
    });
    builder.Services.AddScoped<IShopDbContext>(provider => provider.GetService<ShopDbContext>());

    builder.Services.AddMapper();

    builder.Services.AddIdentityServerServises();
    builder.Services.AddCastomAuthentication();

    builder.Services.AddControllersWithViews();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}