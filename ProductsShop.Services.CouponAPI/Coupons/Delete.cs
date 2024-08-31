using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Services.CouponAPI.Endpoints;
using ProductsShop.Services.CouponAPI.Persistence;

namespace ProductsShop.Services.CouponAPI.Coupons;

public class Delete
{
    public record Response(int Id);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/Coupons/Delete/{id:int}", Handle).WithTags("Coupons");
        }
    }

    public static async Task<Results<Ok<Response>, NotFound>> Handle(int id, AppDbContext context)
    {
        var coupon = await context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
        if (coupon is null)
        {
            return TypedResults.NotFound();
        }

        context.Coupons.Remove(coupon);

        await context.SaveChangesAsync();

        return TypedResults.Ok(new Response(id));
    }
}
