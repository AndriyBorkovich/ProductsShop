using Mapster;
using ProductsShop.Services.CouponAPI.Coupons.CommonModels;
using ProductsShop.Services.CouponAPI.Persistence.Models;

namespace ProductsShop.Services.CouponAPI.Coupons;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Coupon, CouponDTO>().TwoWays();

        config.NewConfig<Create.Request, Coupon>();
    }
}
