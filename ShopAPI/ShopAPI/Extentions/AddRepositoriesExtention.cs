using ShopApi.Model.Interfaces.Repository;
using ShopApi.Model.Repositories;

namespace ShopApi.Extentions
{
    internal static class AddRepositoriesExtention
    {
        internal static void AddRepositories(this IServiceCollection services)
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
        }
    }
}
