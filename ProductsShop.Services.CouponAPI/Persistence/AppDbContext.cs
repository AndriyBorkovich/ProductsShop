using Microsoft.EntityFrameworkCore;
using ProductsShop.Services.CouponAPI.Models;

namespace ProductsShop.Services.CouponAPI.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; }
}
