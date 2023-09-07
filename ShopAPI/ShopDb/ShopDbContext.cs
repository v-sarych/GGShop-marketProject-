using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using ShopDb.Configuration;
using ShopDb.Entities;
using System.Reflection;
using ShopDb.EntitiesConfiguration;

namespace ShopDb
{
    public class ShopDbContext : DbContext, IShopDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<UserShoppingCartItem> UsersShoppingCartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AvailabilityOfProduct> AvailabilityOfProducts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrdersItems { get; set; }

        public ShopDbContext() : base() { }
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<User>(new UserConfiguration());
            builder.ApplyConfiguration<Session>(new SessionConfiguration());
            builder.ApplyConfiguration<UserShoppingCartItem>(new UserShoppingCartItemConfiguration());
            builder.ApplyConfiguration<Comment>(new CommentConfiguration());
            builder.ApplyConfiguration<Order>(new OrderConfiguration());
            builder.ApplyConfiguration<OrderItem>(new OrderItemConfiguration());
            builder.ApplyConfiguration<AvailabilityOfProduct>(new AvailabilityOfProductConfiguration());
            builder.ApplyConfiguration<Tag>(new TagConfiguration());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=192.168.0.15;Port=5432;Database=ShopDb;Username=postgres;Password=1234");
                //используется только при миграциях
            
            base.OnConfiguring(optionsBuilder);
        }


        //все поля которые вы когда либо будете обновлять через эту функцию должны примать значение null в entity
        public async Task<TEntity> UpdatePropertiesWithoutNull<TEntity>(TEntity entity, DbSet<TEntity> set) 
            where TEntity : class
        {   
            set.Attach(entity);

            var entry = this.Entry(entity);

            foreach(var property in entry.OriginalValues.Properties)
                if (entry.OriginalValues[property] != null 
                    && !property.IsKey()
                    && !property.IsForeignKey())
                        entry.Property(property.Name).IsModified = true;

            await this.SaveChangesAsync();
            return entity;
        }// - зарезервированно
    }
}
