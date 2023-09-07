using Microsoft.EntityFrameworkCore;
using ShopDb.Entities;

namespace ShopDb
{
    public interface IShopDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Session> Sessions { get; set; }
        DbSet<UserShoppingCartItem> UsersShoppingCartItems { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<AvailabilityOfProduct> AvailabilityOfProducts { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrdersItems { get; set; }

        Task<TEntity> UpdatePropertiesWithoutNull<TEntity>(TEntity entity, DbSet<TEntity> set)// - зарезервированно
            where TEntity : class;
            //оставляет прежнее значение для null полей обновляемого объекта

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
