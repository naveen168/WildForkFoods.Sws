using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WildForkFoods.Sws.Mobile.Models;
using WildForkFoods.Sws.Mobile.Views;

namespace WildForkFoods.Sws.Mobile.ViewModels
{
    public partial class OrderHistoryViewModel : ObservableObject
    {
        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool isRefreshing = false;

        [ObservableProperty]
        private ObservableCollection<Order> filteredOrders = new();

        private List<Order> _allOrders = new();

        public OrderHistoryViewModel()
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            // Sample data - replace with actual service call
            _allOrders = new List<Order>
            {
                new Order
                {
                    OrderNumber = "924690",
                    Status = "Delivered",
                    OrderDate = DateTime.Now.AddDays(-5),
                    DeliveryDate = DateTime.Now.AddDays(-3),
                    TotalAmount = 150.00m,
                    OrderLines = new ObservableCollection<OrderLine>()
                    {
                        new OrderLine { Sku = "BEEF-001", ProductName = "USDA Prime Ribeye Steak", Quantity = 2, UnitPrice = 25.50m, Unit = "EA" },
                        new OrderLine { Sku = "CHKN-102", ProductName = "Organic Free-Range Chicken Breast", Quantity = 3, UnitPrice = 12.99m, Unit = "EA" },
                        new OrderLine { Sku = "SEAFOOD-205", ProductName = "Wild Caught Atlantic Salmon", Quantity = 2, UnitPrice = 18.99m, Unit = "EA" },
                        new OrderLine { Sku = "PORK-304", ProductName = "Heritage Pork Tenderloin", Quantity = 1, UnitPrice = 15.50m, Unit = "EA" },
                        new OrderLine { Sku = "LAMB-401", ProductName = "New Zealand Lamb Chops", Quantity = 2, UnitPrice = 22.99m, Unit = "EA" },
                        new OrderLine { Sku = "SEAFOOD-310", ProductName = "Jumbo Argentinian Red Shrimp", Quantity = 1, UnitPrice = 19.99m, Unit = "EA" },
                        new OrderLine { Sku = "BEEF-105", ProductName = "Wagyu Beef Burgers", Quantity = 3, UnitPrice = 14.50m, Unit = "EA" },
                        new OrderLine { Sku = "CHKN-201", ProductName = "Chicken Drumsticks", Quantity = 2, UnitPrice = 6.99m, Unit = "EA" },
                        new OrderLine { Sku = "SEAFOOD-415", ProductName = "Ahi Tuna Steaks", Quantity = 2, UnitPrice = 24.99m, Unit = "EA" },
                        new OrderLine { Sku = "BEEF-220", ProductName = "Filet Mignon", Quantity = 4, UnitPrice = 32.99m, Unit = "EA" },
                        new OrderLine { Sku = "PORK-505", ProductName = "Baby Back Ribs", Quantity = 2, UnitPrice = 16.99m, Unit = "EA" },
                        new OrderLine { Sku = "SEAFOOD-520", ProductName = "Sea Scallops", Quantity = 1, UnitPrice = 28.99m, Unit = "EA" },
                        new OrderLine { Sku = "CHKN-305", ProductName = "Chicken Wings", Quantity = 3, UnitPrice = 8.99m, Unit = "EA" },
                        new OrderLine { Sku = "BEEF-325", ProductName = "Strip Steak", Quantity = 2, UnitPrice = 21.99m, Unit = "EA" },
                        new OrderLine { Sku = "LAMB-510", ProductName = "Ground Lamb", Quantity = 2, UnitPrice = 11.99m, Unit = "EA" },
                        new OrderLine { Sku = "SEAFOOD-625", ProductName = "Lobster Tails", Quantity = 4, UnitPrice = 35.99m, Unit = "EA" },
                        new OrderLine { Sku = "PORK-610", ProductName = "Pork Chops", Quantity = 3, UnitPrice = 9.99m, Unit = "EA" },
                        new OrderLine { Sku = "BEEF-430", ProductName = "Brisket", Quantity = 1, UnitPrice = 45.99m, Unit = "EA" },
                    }
                },
                new Order
                {
                    OrderNumber = "952346",
                    Status = "In-Transit",
                    OrderDate = DateTime.Now.AddDays(-2),
                    DeliveryDate = DateTime.Now.AddDays(1),
                    TotalAmount = 250.00m,
                    OrderLines = new ObservableCollection<OrderLine>()
                    {
                        new OrderLine { Sku = "SEAFOOD-730", ProductName = "Mahi Mahi Fillets", Quantity = 3, UnitPrice = 19.99m, Unit = "EA" },
                        new OrderLine { Sku = "BEEF-535", ProductName = "Ground Beef 80/20", Quantity = 5, UnitPrice = 7.99m, Unit = "EA" },
                        new OrderLine { Sku = "CHKN-420", ProductName = "Whole Chicken", Quantity = 2, UnitPrice = 12.99m, Unit = "EA" }
                    }
                },
                new Order
                {
                    OrderNumber = "983103",
                    OrderDate = DateTime.Now.AddDays(1),
                    DeliveryDate = DateTime.Now.AddDays(5),
                    Status = "Pending Approval",
                    StatusColor = "#2196F3",
                    OrderLines = new ObservableCollection<OrderLine>
                    {
                        new OrderLine { Sku = "PORK-715", ProductName = "Italian Sausage Links", Quantity = 3, UnitPrice = 8.99m, Unit = "EA" },
                        new OrderLine { Sku = "SEAFOOD-810", ProductName = "King Crab Legs", Quantity = 2, UnitPrice = 45.99m, Unit = "EA" },
                        new OrderLine { Sku = "BEEF-640", ProductName = "Bone-In Short Ribs", Quantity = 2, UnitPrice = 18.99m, Unit = "EA" }
                    }
                }            
            };

            FilterOrders();
        }

        [RelayCommand]
        private void Search()
        {
            FilterOrders();
        }

        partial void OnSearchTextChanged(string value)
        {
            // Filter as user types
            FilterOrders();
        }

        private void FilterOrders()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                // Show all orders
                FilteredOrders = new ObservableCollection<Order>(_allOrders);
            }
            else
            {
                // Filter by order number or status
                var filtered = _allOrders.Where(o =>
                    o.OrderNumber.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    o.Status.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                FilteredOrders = new ObservableCollection<Order>(filtered);
            }
        }

        [RelayCommand]
        private async Task Refresh()
        {
            IsRefreshing = true;
            try
            {
                // TODO: Reload orders from service
                await Task.Delay(1000); // Simulate API call
                LoadOrders();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task ViewOrderDetails(Order order)
        {
            if (order == null) return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Order", order }
            };
            await Shell.Current.GoToAsync($"//{nameof(OrderDetailsPage)}", navigationParameter);
        }
    }
}