using ShopApiCore.Interfaces.Repository;
using ShopApiCore.Repositories;

namespace ShopApiServer.Extentions
{
    internal static class AddRepositoriesExtention
    {
        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductControllRepository, ProductControllRepository>();
            services.AddScoped<IProductAvailabilityRepository, ProductAvailabilityRepository>();
            services.AddScoped<IProductSearchRepository, ProductSearchRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<ITagRepository, TagRepository>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IStatisticsRepository, StatisticsRepository>();

            return services;
        }
    }
}
