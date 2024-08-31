using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Services.CouponAPI.Endpoints;
using ProductsShop.Services.CouponAPI.Persistence;

namespace ProductsShop.Services.CouponAPI.Coupons;

public static class GetById
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
            app.MapGet("Coupons/GetById/{id:int}", Handler).WithTags("Coupons");
        }
    }

    public static async Task<Results<Ok<Response>, NotFound>> Handler(int id, AppDbContext context)
    {
        var coupon = await context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);

        if (coupon is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new Response(
            coupon.CouponId, 
            coupon.CouponName,
            coupon.CouponCode,
            coupon.DiscountAmount,
            coupon.MinAmount));
    }
}
