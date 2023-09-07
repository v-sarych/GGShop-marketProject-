using IdentityServer.Model;
using IdentityServer.Model.interfaces;
using IdentityServer.Model.interfaces.Repositories;
using IdentityServer.Model.Extentions;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Model.Mapper;
using ShopDb;
using ShopApi.Extentions;

var builder = WebApplication.CreateBuilder(args);

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