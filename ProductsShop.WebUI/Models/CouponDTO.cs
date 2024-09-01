using System;

namespace ProductsShop.WebUI.Models;

public record CouponDTO
{
    public int CouponId { get; set; }
    public string CouponName { get; set; }
    public string CouponCode { get; set; }
    public double DiscountAmount { get; set; }
    public int MinAmount { get; set; }
}
