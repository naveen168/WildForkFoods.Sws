using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WildForkFoods.Sws.Mobile.Models;
using WildForkFoods.Sws.Mobile.Services;
using System.Collections.ObjectModel;

namespace WildForkFoods.Sws.Mobile.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        private readonly CartService _cartService;

        [ObservableProperty]
        private string storeId = string.Empty;

        [ObservableProperty]
        private DateTime deliveryDate = DateTime.Today.AddDays(1);

        [ObservableProperty]
        private DateTime minimumDeliveryDate = DateTime.Today.AddDays(1);

        [ObservableProperty] private ObservableCollection<ReplenishmentLine> lines = new();
        [ObservableProperty] private string reference = $"Crew {DateTime.Now:MM/dd HH:mm}";
        [ObservableProperty] private string status = "";

        public bool HasItems => Lines?.Count > 0;
        public bool IsEmpty => !HasItems;

        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
            
            // Load Store ID from user profile
            LoadStoreId();
        }

        private void LoadStoreId()
        {
            // TODO: Get from user preferences/authentication service
            // For now, using a placeholder. You should replace this with actual user profile data
            // Example: StoreId = Preferences.Get("SelectedStoreId", "");
            // or from a UserService/AuthenticationService
            
            StoreId = "1094"; // Placeholder - replace with actual user store ID
        }

        public void Refresh()
        {
            Lines.Clear();
            foreach (var line in _cartService.Lines)
            {
                Lines.Add(line);
            }
            UpdateCartStatus();
        }

        private void UpdateCartStatus()
        {
            OnPropertyChanged(nameof(HasItems));
            OnPropertyChanged(nameof(IsEmpty));
        }

        [RelayCommand] 
        private void Remove(ReplenishmentLine line) 
        { 
            _cartService.Remove(line.Sku, line.Unit);
            Lines.Remove(line);
            UpdateCartStatus();
        }

        [RelayCommand]
        private void Increment(ReplenishmentLine line)
        {
            line.Quantity++;
            _cartService.Update(line.Sku, line.Unit, line.Quantity);
        }

        [RelayCommand]
        private void Decrement(ReplenishmentLine line)
        {
            if (line.Quantity > 1)
            {
                line.Quantity--;
                _cartService.Update(line.Sku, line.Unit, line.Quantity);
            }
            else
            {
                _cartService.Remove(line.Sku, line.Unit);
                Lines.Remove(line);
                UpdateCartStatus();
            }
        }

        [RelayCommand]
        private async Task Submit()
        {
            if (!_cartService.Lines.Any()) { Status = "Cart is empty"; return; }

            var order = new ReplenishmentOrder(
                StoreId: StoreId,
                UserId: "user1",
                CreatedUtc: DateTime.UtcNow,
                Lines: _cartService.Lines.ToList(),
                Reference: Reference);

            // Demo: just clear and show alert (no endpoints)
            _cartService.Clear();
            Lines.Clear();
            Status = "Order submitted (demo)";
            UpdateCartStatus();
            await Shell.Current.DisplayAlert("Order", "Order submitted (demo, no API).", "OK");
        }
    }
}
