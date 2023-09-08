using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDb.Entities;

namespace ShopDb.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasPrincipalKey(y => y.Id)
                .HasForeignKey(z => z.OrderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
