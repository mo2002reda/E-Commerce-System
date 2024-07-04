using Microsoft.EntityFrameworkCore;
using SkelandStore.Core.Entities;
using SkelandStore.Core.Entities.Order_Aggregation;
using System.Reflection;

namespace SkyLand.Repository.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {//using constructor chaining to allow dependancy injection
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//execute all configration classes which implement interface IEntityTypeConfigration
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<DeleveryMethod> DeleveryMethods { get; set; }
    }
}
