using WildForkFoods.Sws.Mobile.ViewModels;

namespace WildForkFoods.Sws.Mobile.Views;

public partial class UserPage : ContentPage
{

    public UserPage(UserViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}