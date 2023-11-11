using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShopDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDb.EntitiesConfiguration
{
    internal class PackageSizeConfiguration : IEntityTypeConfiguration<PackageSize>
    {
        public void Configure(EntityTypeBuilder<PackageSize> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasAlternateKey(x => new {x.Width, x.Length, x.Height});
        }
    }
}
