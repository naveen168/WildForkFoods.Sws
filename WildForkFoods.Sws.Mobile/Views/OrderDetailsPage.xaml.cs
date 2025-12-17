using WildForkFoods.Sws.Mobile.ViewModels;

namespace WildForkFoods.Sws.Mobile.Views
{
    public partial class OrderDetailsPage : ContentPage
    {
        public OrderDetailsPage(OrderDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}