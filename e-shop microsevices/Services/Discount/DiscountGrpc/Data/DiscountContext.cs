using DiscountGrpc.Model;
using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext>options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, Name = "Iphone X", Description = "Iphone Discount Coupon", Amount = 200 },
                new Coupon { Id = 2, Name = "Samsung", Description = "Samsung Discount Coupon", Amount = 300 }
            );
        }
    }
}
