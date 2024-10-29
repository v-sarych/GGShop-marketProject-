using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDb.Entities;

namespace ShopDb.EntitiesConfiguration;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type).IsRequired();

        builder.HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .HasPrincipalKey(y => y.Id)
            .HasForeignKey(z => z.OrderId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Payment)
            .WithOne(x => x.Order)
            .HasPrincipalKey<Order>(x => x.Id)
            .HasForeignKey<Payment>(x => x.Id)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}