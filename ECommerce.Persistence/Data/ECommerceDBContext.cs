using ECommerce.Persistence.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Data
{
    public class ECommerceDBContext : DbContext
    {
        public ECommerceDBContext(DbContextOptions<ECommerceDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(v => v.Variants).
                WithOne(p => p.Product).
                HasForeignKey(p => p.ID)
              ;
            modelBuilder.Entity<Variant>()
                .HasMany(v => v.Stocks)
                .WithOne(v => v.Variant)
                .HasForeignKey(s => s.ID);
            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.Stocks)
                .WithOne(s => s.Warehouse)
                .HasForeignKey(s => s.ID);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
    }
}
