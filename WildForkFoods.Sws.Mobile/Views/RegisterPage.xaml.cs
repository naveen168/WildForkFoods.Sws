using WildForkFoods.Sws.Mobile.ViewModels;

namespace WildForkFoods.Sws.Mobile.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
