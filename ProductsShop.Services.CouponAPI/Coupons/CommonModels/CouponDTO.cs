using System;

namespace ProductsShop.Services.CouponAPI.Coupons.CommonModels;

public record CouponDTO(
        int CouponId,
        string CouponName,
        string CouponCode,
        decimal DiscountAmount,
        int MinAmount
    );