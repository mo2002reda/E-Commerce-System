using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkelandStore.Core.Entities.Order_Aggregation;
using System.Net.NetworkInformation;

namespace SkyLand.Repository.Data.Configrations
{
    public class OrderStatusConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            #region Configration Of Converting Enum Value
            builder.Property(O => O.OrderStatus)
                      .HasConversion(OStatus => OStatus.ToString(), OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));
            #region HasConvention :   
            /*Is built In Function Used To Convert From Type To Type
            In This Case It Used To Convert From Integer (Indexes Of Enum) To String (Values Of Enum) To Can Store It In Data Base
            It Take 3 Parameters : 1)The type which want To Convert To it
                                   2)The Type Which Convert From 
                                   3)The Value which want to Convert
            */
            #endregion 
            #endregion

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            #region ConFiger Address Properity
            //Address[Not Table] => Order + Address =>[One Table]
            //The Owner = Order Table 
            builder.OwnsOne(O => O.ShippingAddress, S => S.WithOwner());
            #endregion

            builder.HasOne(O => O.DeleveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
