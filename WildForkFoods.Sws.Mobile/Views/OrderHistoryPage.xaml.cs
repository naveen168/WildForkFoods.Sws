using WildForkFoods.Sws.Mobile.ViewModels;

namespace WildForkFoods.Sws.Mobile.Views;

public partial class OrderHistoryPage : ContentPage
{
    public OrderHistoryPage(OrderHistoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    //private async void ScanClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(ScanPage));
    //private async void SearchClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(SearchPage));
    //private async void CartClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(CartPage));
}