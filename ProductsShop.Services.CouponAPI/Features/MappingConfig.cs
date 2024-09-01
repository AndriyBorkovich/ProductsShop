using Mapster;
using ProductsShop.Services.CouponAPI.Features.CommonModels;
using ProductsShop.Services.CouponAPI.Persistence.Models;

namespace ProductsShop.Services.CouponAPI.Features;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Coupon, CouponDTO>().TwoWays();

        config.NewConfig<Create.Request, Coupon>();
    }
}
