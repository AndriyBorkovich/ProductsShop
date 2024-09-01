using BlazorBootstrap;

namespace ProductsShop.WebUI.Utils
{
    public static class ToastMessageFactory
    {
        public static ToastMessage Create(ToastType toastType, bool isSuccess, string message)
        {
            return new ToastMessage
            {
                Type = toastType,
                Title = isSuccess ? "Success" : "Error",
                HelpText = $"{DateTime.Now}",
                Message = message,
            };
        }
    }
}
