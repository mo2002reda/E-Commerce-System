using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkelandStore.Core.Entities;

namespace SkyLand.Repository.Data.Configrations
{
    public class ProductBrandConfigrations : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(p => p.Name).IsRequired()
                   .HasMaxLength(100);
        }
    }
}
