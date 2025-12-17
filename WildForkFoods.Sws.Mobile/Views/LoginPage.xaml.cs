using WildForkFoods.Sws.Mobile.ViewModels;

namespace WildForkFoods.Sws.Mobile.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}