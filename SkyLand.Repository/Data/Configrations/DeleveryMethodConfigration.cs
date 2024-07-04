using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkelandStore.Core.Entities.Order_Aggregation;

namespace SkyLand.Repository.Data.Configrations
{
    public class DeleveryMethodConfigration : IEntityTypeConfiguration<DeleveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeleveryMethod> builder)
        {
            builder.Property(C => C.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
