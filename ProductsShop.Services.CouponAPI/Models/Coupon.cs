using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShop.Services.CouponAPI.Models;

public class Coupon
{
    public int CouponId { get; set; }
    [Required ,StringLength(200)]
    public string CouponName { get; set; }
    [Required, StringLength(200)]
    public string CouponCode { get; set; }
    [Precision(18, 2)]
    public decimal DiscountAmount { get; set; }
    public int MinAmount { get; set; }
}
