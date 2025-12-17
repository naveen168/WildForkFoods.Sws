using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WildForkFoods.Sws.Mobile.Models;
using WildForkFoods.Sws.Mobile.Services;

namespace WildForkFoods.Sws.Mobile.ViewModels
{
    public partial class ScanViewModel : ObservableObject
    {
        private readonly ProductService _products;
        private readonly CartService _cart;

        [ObservableProperty] private string productName = "Scan a barcode";
        [ObservableProperty] private int quantity = 1;
        public List<string> Units { get; } = new() { "EA" };
        [ObservableProperty] private string selectedUnit = "EA";
        [ObservableProperty] private string status = "Test";

        private Product? _current;

        public ScanViewModel(ProductService products, CartService cart)
        { _products = products; _cart = cart; }

        //[RelayCommand]
        //private void Add()
        //{
        //    if (_current is null) { status = "No product selected"; return; }
        //    if (quantity <= 0) { status = "Quantity must be > 0"; return; }
        //    _cart.AddOrUpdate(_current.Sku, selectedUnit, quantity);
        //    status = $"Added {_current.Name} x {quantity} {selectedUnit}";
        //    quantity = 1;
        //}

        public async Task OnBarcodeAsync(string upc)
        {
            Status = "Looking up product…";
            _current = await _products.GetByUpcAsync(upc);
            ProductName = _current is null ? "UPC not found" : _current.Name;
            Status = _current is null ? "Not found" : "Ready to add";
        }
    }
}
