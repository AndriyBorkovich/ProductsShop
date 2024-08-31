using Microsoft.EntityFrameworkCore;
using ProductsShop.Services.CouponAPI.Endpoints;
using ProductsShop.Services.CouponAPI.Persistence;

namespace ProductsShop.Services.CouponAPI.Coupons;

public static class GetAll
{
    public record Response(
            int CouponId,
            string CouponName,
            string CouponCode,
            decimal DiscountAmount,
            int MinAmount
        );

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Coupons", Handler).WithTags("Coupons");
        }
    }

    public static async Task<IResult> Handler(AppDbContext context)
    {
        return TypedResults.Ok(await context.Coupons
            .Select(c => new Response(
                c.CouponId,
                c.CouponName,
                c.CouponCode,
                c.DiscountAmount,
                c.MinAmount
            )).ToListAsync());
    }
}
