using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WildForkFoods.Sws.Mobile.Models;
using WildForkFoods.Sws.Mobile.Services;
using WildForkFoods.Sws.Mobile.Views;

namespace WildForkFoods.Sws.Mobile.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        private readonly ProductService _products;
        private readonly CartService _cart;

        [ObservableProperty] 
        private string query = "";
        
        [ObservableProperty] 
        private List<ProductViewModel> results = new();

        public SearchViewModel(ProductService products, CartService cart)
        { 
            _products = products; 
            _cart = cart; 
        }

        partial void OnQueryChanged(string value) => _ = SearchAsync(value);

        private async Task SearchAsync(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) 
            { 
                Results = new(); 
                return; 
            }
            
            var products = await _products.SearchAsync(value);
            Results = products.Select(p => new ProductViewModel(p)).ToList();
        }

        [RelayCommand]
        private void Add(ProductViewModel productViewModel)
        {
            if (productViewModel.IsAdded) return; // Already added
            
            _cart.Add(productViewModel.Name, productViewModel.Sku, "EA", 1);
            productViewModel.MarkAsAdded();
        }

        [RelayCommand]
        private async Task ScanBarcode()
        {
            await Shell.Current.GoToAsync($"//{nameof(ScanPage)}");
        }
    }
}
