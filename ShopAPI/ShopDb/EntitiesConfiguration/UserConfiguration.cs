using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDb.Entities;
using ShopDb.Enums;

namespace ShopDb.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Role)
                .HasDefaultValue(Roles.User);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasDefaultValue("Noname");

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.Password)
                .IsRequired().
                HasMaxLength(30);

            builder.HasMany(x => x.UserShoppingCartItems)
                .WithOne()
                .HasForeignKey(y => y.UserId)
                .HasPrincipalKey(z => z.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.WrittenComments)
                .WithOne(x => x.User)
                .HasPrincipalKey (z => z.Id)
                .HasForeignKey(y => y.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Sessions)
                .WithOne()
                .HasForeignKey(y => y.UserId)
                .HasPrincipalKey(z => z.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Orders)
                .WithOne(y => y.User)
                .HasForeignKey(z => z.UserId)
                .HasPrincipalKey(v => v.Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
