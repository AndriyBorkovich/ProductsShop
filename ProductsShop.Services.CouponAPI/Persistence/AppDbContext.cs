using Microsoft.EntityFrameworkCore;
using ProductsShop.Services.CouponAPI.Persistence.Models;

namespace ProductsShop.Services.CouponAPI.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().Property(c => c.CouponId).UseIdentityAlwaysColumn();
        base.OnModelCreating(modelBuilder);
    }
}
