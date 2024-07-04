using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkelandStore.Core.Entities;

namespace SkyLand.Repository.Data.Configrations
{
    public class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                   .WithMany()
                   .HasForeignKey(p => p.ProductBrandId);

            #region change onDelete Behivior
            //.OnDelete(DeleteBehavior.SetNull) : change cascade behivior cause type of forignKey is int (not allow null) so if user delete any Brand it will delete all Products on this Brand 
            //if we do this we must change type of forignKey on the Product Table to be Nullable 
            #endregion

            builder.HasOne(p => p.ProductType)
                   .WithMany()
                   .HasForeignKey(p => p.ProductTypeId);
            //make it Required incase will upgrade version of the project to .net5 and the string is nullable in .net5
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Description)
                   .IsRequired();

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
