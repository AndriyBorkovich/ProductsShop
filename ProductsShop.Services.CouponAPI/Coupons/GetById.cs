using MapsterMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Services.CouponAPI.Coupons.CommonModels;
using ProductsShop.Services.CouponAPI.Endpoints;
using ProductsShop.Services.CouponAPI.Persistence;

namespace ProductsShop.Services.CouponAPI.Coupons;

public static class GetById
{

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Coupons/GetById/{id:int}", Handler).WithTags("Coupons");
        }
    }

    public static async Task<Results<Ok<CouponDTO>, NotFound>> Handler(int id, AppDbContext context, IMapper mapper)
    {
        var coupon = await context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);

        if (coupon is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mapper.Map<CouponDTO>(coupon));
    }
}
