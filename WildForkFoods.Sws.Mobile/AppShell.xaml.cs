using WildForkFoods.Sws.Mobile.Views;

namespace WildForkFoods.Sws.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(OrderDetailsPage), typeof(OrderDetailsPage));
            Routing.RegisterRoute(nameof(ScanPage), typeof(ScanPage));

        }
    }
}
