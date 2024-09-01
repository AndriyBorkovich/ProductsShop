namespace ProductsShop.Services.CouponAPI.Features.CommonModels;

public record CouponDTO(
    int CouponId,
    string CouponName,
    string CouponCode,
    decimal DiscountAmount,
    int MinAmount
);