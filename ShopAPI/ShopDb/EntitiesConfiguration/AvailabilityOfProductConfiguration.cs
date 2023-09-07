﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDb.Entities;

namespace ShopDb.Configuration
{
    internal class AvailabilityOfProductConfiguration : IEntityTypeConfiguration<AvailabilityOfProduct>
    {
        public void Configure(EntityTypeBuilder<AvailabilityOfProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasAlternateKey(x => new { x.ProductId, x.Size });

            builder.HasOne(x => x.Product)
                .WithMany(y => y.AvailabilitisOfProduct)
                .HasPrincipalKey(z => z.Id)
                .HasForeignKey(v => v.ProductId);

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
