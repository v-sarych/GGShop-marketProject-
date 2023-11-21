using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDb.Entities;

namespace ShopDb.Configuration
{
    internal class AvailabilityOfProductConfiguration : IEntityTypeConfiguration<AvailabilityOfProduct>
    {
        public void Configure(EntityTypeBuilder<AvailabilityOfProduct> builder)
        {
            builder.HasKey(x => x.Sku);
            builder.HasAlternateKey(x => new { x.ProductId, x.Size });

            builder.HasOne(x => x.Product)
                .WithMany(y => y.AvailabilitisOfProduct)
                .HasPrincipalKey(z => z.Id)
                .HasForeignKey(v => v.ProductId);

            builder.HasOne(x => x.PackageSize)
                .WithMany()
                .HasForeignKey(v => v.PackageSizeId)
                .HasPrincipalKey(z => z.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.PackageSizeId)
                .IsRequired();

            builder.Property(x => x.Weight)
                .IsRequired();

            builder.Property(x => x.Size)
                .IsRequired()
                .HasDefaultValue("without size");

            builder.Property(x => x.Cost)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.Count)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}
