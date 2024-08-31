using Bogus;
using ProductsShop.Services.CouponAPI.Persistence.Models;

namespace ProductsShop.Services.CouponAPI.Persistence;

public interface IDataSeeder
{
    Task SeedCoupons();
}

public class DataSeeder(IServiceProvider serviceProvider) : IDataSeeder
{
    public async Task SeedCoupons()
    {
        var couponFaker = new Faker<Coupon>()
            .RuleFor(c => c.CouponName, f => f.Commerce.ProductName())
            .RuleFor(c => c.CouponCode, f => f.Commerce.Ean13())
            .RuleFor(c => c.DiscountAmount, f => f.Finance.Amount(1, 100, 2))
            .RuleFor(c => c.MinAmount, f => f.Random.Int(50, 500));

        var coupons = couponFaker.Generate(10);

        using var dbcontext = serviceProvider.GetRequiredService<AppDbContext>();

        await dbcontext.Coupons.AddRangeAsync(coupons);
        await dbcontext.SaveChangesAsync();
    }
}
