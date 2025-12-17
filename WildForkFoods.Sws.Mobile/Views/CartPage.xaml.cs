using WildForkFoods.Sws.Mobile.ViewModels;

namespace WildForkFoods.Sws.Mobile.Views;

public partial class CartPage : ContentPage
{
    private readonly CartViewModel _viewModel;

    public CartPage(CartViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Assuming you add a public Refresh method or make the existing one public
        _viewModel.Refresh();
    }
}   