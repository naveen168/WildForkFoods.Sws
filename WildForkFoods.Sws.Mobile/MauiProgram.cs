using Microsoft.Extensions.Logging;
using WildForkFoods.Sws.Mobile.ViewModels;
using WildForkFoods.Sws.Mobile.Views;
using ZXing.Net.Maui.Controls;

namespace WildForkFoods.Sws.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Services
            builder.Services.AddSingleton<Services.ProductService>();
            builder.Services.AddSingleton<Services.CartService>();
            builder.Services.AddSingleton<Services.UserService>();

            // ViewModels
            builder.Services.AddTransient<OrderHistoryViewModel>();
            builder.Services.AddTransient<ScanViewModel>();
            builder.Services.AddTransient<SearchViewModel>();
            builder.Services.AddTransient<CartViewModel>();
            builder.Services.AddTransient<OrderDetailsViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<UserViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();

            // Pages
            builder.Services.AddTransient<OrderHistoryPage>();
            builder.Services.AddTransient<ScanPage>();
            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddTransient<CartPage>();
            builder.Services.AddTransient<OrderDetailsPage>();
            builder.Services.AddTransient<UserPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();

            return builder.Build();
        }
    }
}
