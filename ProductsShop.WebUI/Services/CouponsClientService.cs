using ProductsShop.WebUI.Models;

namespace ProductsShop.WebUI.Services;

public interface ICouponsClientService
{
    Task<List<CouponDTO>> GetAllCouponsAsync();
    Task<CouponDTO> GetCouponByIdAsync(int id);
    Task<CouponDTO> GetCouponByCodeAsync(string code);
    Task<bool> CreateCouponAsync(CouponDTO coupon);
    Task<bool> UpdateCouponAsync(int id, CouponDTO coupon);
    Task<bool> DeleteCouponAsync(int id);
}


public class CouponsClientService(HttpClient httpClient) : ICouponsClientService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<CouponDTO>> GetAllCouponsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CouponDTO>>("api/Coupons");
    }

    public async Task<CouponDTO> GetCouponByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CouponDTO>($"api/Coupons/GetById/{id}");
    }

    public async Task<CouponDTO> GetCouponByCodeAsync(string code)
    {
        return await _httpClient.GetFromJsonAsync<CouponDTO>($"api/Coupons/GetByCode/{code}");
    }

    public async Task<bool> CreateCouponAsync(CouponDTO coupon)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Coupons/Create", coupon);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCouponAsync(int id, CouponDTO coupon)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Coupons/Edit/{id}", coupon);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCouponAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Coupons/Delete/{id}");
        return response.IsSuccessStatusCode;
    }
}
