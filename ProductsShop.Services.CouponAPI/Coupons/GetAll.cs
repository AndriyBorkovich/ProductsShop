using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Services.CouponAPI.Coupons.CommonModels;
using ProductsShop.Services.CouponAPI.Endpoints;
using ProductsShop.Services.CouponAPI.Persistence;

namespace ProductsShop.Services.CouponAPI.Coupons;

public static class GetAll
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/Coupons", Handler).WithTags("Coupons").Produces<List<CouponDTO>>();
        }
    }

    public static async Task<IResult> Handler(AppDbContext context, IMapper mapper)
    {
        var coupons = await context.Coupons.AsNoTracking().ToListAsync();
        return TypedResults.Ok(mapper.Map<List<CouponDTO>>(coupons));
    }
}
