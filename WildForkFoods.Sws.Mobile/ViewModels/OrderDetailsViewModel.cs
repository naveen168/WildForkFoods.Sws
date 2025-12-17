using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WildForkFoods.Sws.Mobile.Models;
using WildForkFoods.Sws.Mobile.Services;
using WildForkFoods.Sws.Mobile.Views;

namespace WildForkFoods.Sws.Mobile.ViewModels
{       
    [QueryProperty(nameof(Order), "Order")]
    public partial class OrderDetailsViewModel : ObservableObject
    {
        private readonly CartService _cartService;

        [ObservableProperty]
        private Order order;

        [ObservableProperty]
        private bool isStoreManager;

        public OrderDetailsViewModel(CartService cartService)
        {
            _cartService = cartService;     
            
            // Check if current user is a Store Manager
            // You can get this from a UserService, Preferences, or SecureStorage
            // For now, getting it from UserViewModel if available
            CheckUserRole();
        }

        private void CheckUserRole()
        {
            // Option 1: From Preferences (if stored during login)
            var userRole = Preferences.Get("UserRole", string.Empty);
            IsStoreManager = userRole == "Store Manager";
            
            // Option 2: If you have a UserService, inject it and use:
            // IsStoreManager = _userService.CurrentUser?.Role == "Store Manager";
        }

        [RelayCommand]
        private async Task AddToCart(OrderLine orderLine)
        {
            _cartService.Add(orderLine.ProductName, orderLine.Sku, orderLine.Unit, orderLine.Quantity);
            await Shell.Current.DisplayAlert("Success", $"{orderLine.ProductName} added to cart", "OK");
        }

        [RelayCommand]
        private void Remove(OrderLine orderLine)
        {
            Order.OrderLines.Remove(orderLine);
            // Update total amount
            Order.TotalAmount = Order.OrderLines.Sum(l => l.LineTotal);
        }

        [RelayCommand]
        private async Task ReOrder()
        {
            if (Order?.OrderLines == null || !Order.OrderLines.Any())
            {
                await Shell.Current.DisplayAlert("Error", "No items to re-order", "OK");
                return;
            }

            // Add all order items to the cart
            foreach (var orderLine in Order.OrderLines)
            {
                _cartService.Add(orderLine.ProductName, orderLine.Sku, orderLine.Unit, orderLine.Quantity);
            }

            // Show confirmation and navigate to cart
            bool navigateToCart = await Shell.Current.DisplayAlert(
                "Success", 
                $"{Order.OrderLines.Count} item(s) added to cart. Would you like to view your cart?", 
                "View Cart", 
                "Continue Shopping");

            if (navigateToCart)
            {
                await Shell.Current.GoToAsync($"//{nameof(CartPage)}");
            }
        }

        [RelayCommand]
        private async Task Approve()
        {
            if (Order == null)
            {
                await Shell.Current.DisplayAlert("Error", "No order to approve", "OK");
                return;
            }

            bool confirm = await Shell.Current.DisplayAlert(
                "Approve Order",
                $"Are you sure you want to approve order {Order.OrderNumber}?",
                "Approve",
                "Cancel");

            if (confirm)
            {
                try
                {
                    // TODO: Call your API service to approve the order
                    // await _orderService.ApproveOrder(Order.OrderNumber);
                    
                    // Update order status locally
                    Order.Status = "Approved";
                    
                    await Shell.Current.DisplayAlert(
                        "Success",
                        $"Order {Order.OrderNumber} has been approved",
                        "OK");
                    
                    // Optionally navigate back
                    await Shell.Current.GoToAsync("..");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert(
                        "Error",
                        $"Failed to approve order: {ex.Message}",
                        "OK");
                }
            }
        }
    }
}