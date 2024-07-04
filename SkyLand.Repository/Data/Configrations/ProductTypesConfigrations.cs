using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkelandStore.Core.Entities;

namespace SkyLand.Repository.Data.Configrations
{
    public class ProductTypesConfigrations : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
