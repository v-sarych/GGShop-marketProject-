using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDb.Entities;

namespace ShopDb.EntitiesConfiguration;

internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => new {x.ProductId, x.UserId});

        builder.Property(x => x.Text).IsRequired();

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Comments)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.ProductId);
    }
}