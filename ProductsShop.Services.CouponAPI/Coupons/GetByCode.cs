using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Services.CouponAPI.Endpoints;
using ProductsShop.Services.CouponAPI.Persistence;

namespace ProductsShop.Services.CouponAPI.Coupons;

public class GetByCode
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
            app.MapGet("Coupons/GetByCode/{code}", Handler).WithTags("Coupons");
        }
    }

    public static async Task<Results<Ok<Response>, NotFound>> Handler(string code, AppDbContext context)
    {
        // perform case-insentitive search
        var coupon = await context.Coupons.FirstOrDefaultAsync(c => EF.Functions.ILike(c.CouponCode, code));

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
