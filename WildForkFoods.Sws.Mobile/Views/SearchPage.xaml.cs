using CommunityToolkit.Mvvm.Input;
using WildForkFoods.Sws.Mobile.ViewModels;

namespace WildForkFoods.Sws.Mobile.Views;

public partial class SearchPage : ContentPage
{
    public SearchPage(SearchViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

}