using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductsShop.Services.CouponAPI.Endpoints;
using ProductsShop.Services.CouponAPI.Persistence;
using ProductsShop.Services.CouponAPI.Persistence.Models;

namespace ProductsShop.Services.CouponAPI.Coupons;

public static class Create
{
    public record Request(
        string CouponName,
        string CouponCode,
        decimal DiscountAmount,
        int MinAmount
    );

    public record Response(int Id);

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
            app.MapPost("api/Coupons/Create", Handler).WithTags("Coupons");
        }
    }

    public static async Task<Results<Ok<Response>, BadRequest<List<ValidationFailure>>>> Handler(
        [FromBody] Request createRequest,
        IValidator<Request> validator,
        AppDbContext context,
        IMapper mapper)
    {
        var validationResult = await validator.ValidateAsync(createRequest);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.Errors);
        }

        var newCoupon = mapper.Map<Coupon>(createRequest);

        await context.Coupons.AddAsync(newCoupon);
        await context.SaveChangesAsync();

        return TypedResults.Ok(new Response(newCoupon.CouponId));
    }
}
