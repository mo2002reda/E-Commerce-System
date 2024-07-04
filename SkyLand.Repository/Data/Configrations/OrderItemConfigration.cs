using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkelandStore.Core.Entities.Order_Aggregation;

namespace SkyLand.Repository.Data.Configrations
{
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
            builder.OwnsOne(O => O.Product, I => I.WithOwner());//mapping all Prop Of Product with The Owner



        }
    }
}
