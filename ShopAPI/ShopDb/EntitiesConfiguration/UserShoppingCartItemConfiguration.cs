using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDb.Entities;

namespace ShopDb.Configuration
{
    internal class UserShoppingCartItemConfiguration : IEntityTypeConfiguration<UserShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<UserShoppingCartItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasAlternateKey(x => new { x.UserId, x.Sku});

            builder.HasOne(x => x.AvailabilityOfProduct)
                .WithMany(y => y.UserShoppingCartItems)
                .HasPrincipalKey(z => z.Sku)
                .HasForeignKey(v => v.Sku);
        }
    }
}
