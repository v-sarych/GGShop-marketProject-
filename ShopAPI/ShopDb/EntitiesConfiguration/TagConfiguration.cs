using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDb.Entities;

namespace ShopDb.EntitiesConfiguration;

internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(x => x.Name);

        builder.HasMany(x => x.Products)
            .WithMany(x => x.Tags)
            .UsingEntity<ProductTag>(
                l => l.HasOne(t => t.Product).WithMany(t => t.ProductTags).OnDelete(DeleteBehavior.Cascade),
                r => r.HasOne(p => p.Tag).WithMany(p => p.ProductsTag).OnDelete(DeleteBehavior.Cascade),
                x =>
                {
                    x.HasKey(y => new { y.ProductId, y.TagId });
                }
            );
    }
}