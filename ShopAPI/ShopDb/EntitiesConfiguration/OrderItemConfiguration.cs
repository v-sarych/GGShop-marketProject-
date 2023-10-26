using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDb.Entities;

namespace ShopDb.Configuration
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.Sku });

            builder.HasOne(x => x.Product)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(y => y.ProductId)
                .HasPrincipalKey(z => z.Id)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.AvailabilityOfProduct)
                .WithOne()
                .HasForeignKey
        }
    }
}
