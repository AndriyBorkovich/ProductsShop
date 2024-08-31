using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Services.CouponAPI.Coupons.CommonModels;
using ProductsShop.Services.CouponAPI.Endpoints;
using ProductsShop.Services.CouponAPI.Persistence;

namespace ProductsShop.Services.CouponAPI.Coupons;

public class Edit
{
    public record Request(
        string CouponName,
        string CouponCode,
        decimal DiscountAmount,
        int MinAmount
    );

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.CouponName).MinimumLength(1).MaximumLength(200);
            RuleFor(r => r.CouponCode).MinimumLength(3).MaximumLength(200);
            RuleFor(r => r.DiscountAmount).GreaterThan(0.0m);
            RuleFor(r => r.MinAmount).GreaterThan(0);
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("api/Coupons/Edit/{id:int}", Handler).WithTags("Coupons");
        }
    }

    public static async Task<Results<Ok<CouponDTO>, NotFound, BadRequest<List<ValidationFailure>>>> Handler(
        int id,
        [FromBody] Request editRequest,
        IValidator<Request> validator,
        AppDbContext context,
        IMapper mapper)
    {
        var coupon = await context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
        if (coupon is null)
        {
            return TypedResults.NotFound();
        }

        var validationResult = await validator.ValidateAsync(editRequest);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.Errors);
        }

        mapper.Map(editRequest, coupon);

        context.Coupons.Update(coupon);
        await context.SaveChangesAsync();

        return TypedResults.Ok(mapper.Map<CouponDTO>(coupon));
    }
}
