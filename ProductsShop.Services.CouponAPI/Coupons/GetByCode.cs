using MapsterMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Services.CouponAPI.Coupons.CommonModels;
using ProductsShop.Services.CouponAPI.Endpoints;
using ProductsShop.Services.CouponAPI.Persistence;

namespace ProductsShop.Services.CouponAPI.Coupons;

public static class GetByCode
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("Coupons/GetByCode/{code}", Handler).WithTags("Coupons");
        }
    }

    public static async Task<Results<Ok<CouponDTO>, NotFound>> Handler(string code, AppDbContext context, IMapper mapper)
    {
        // perform case-insentitive search
        var coupon = await context.Coupons.FirstOrDefaultAsync(c => EF.Functions.ILike(c.CouponCode, code));

        if (coupon is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mapper.Map<CouponDTO>(coupon));
    }
}
