using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WildForkFoods.Sws.Mobile.Models;
using WildForkFoods.Sws.Mobile.Views;

namespace WildForkFoods.Sws.Mobile.ViewModels
{
    [QueryProperty(nameof(User), "User")]
    public partial class UserViewModel : ObservableObject
    {
        [ObservableProperty]
        private User? user;

        [ObservableProperty]
        private string userName = "John Doe";

        [ObservableProperty]
        private string email = "john.doe@wildforkfoods.com";
                
        [ObservableProperty]
        private string role = "Store Manager";

        [ObservableProperty]
        private List<Store> availableStores = new List<Store>()
        {
            new Store { StoreId = "1094", Name = "Sobeys Reenders" },
            new Store { StoreId = "1099", Name = "Sobeys St. Vital" },
            new Store { StoreId = "1100", Name = "Sobeys Main Street" }
        };

        [ObservableProperty]
        private Store? selectedStore;
            
        //public UserViewModel()
        //{
        //    // Set default selected store
        //    SelectedStore = AvailableStores.FirstOrDefault();
        //}

        partial void OnUserChanged(User? value)
        {
            if (value != null)
            {
                // Update properties from received user object
                UserName = value.Name ?? "John Doe";
                Email = value.Email ?? "john.doe@wildforkfoods.com";
                Role = value.Role ?? "Store Manager";
                
                // Set selected store from user's store
                if (!string.IsNullOrEmpty(value.StoreId))
                {
                    SelectedStore = AvailableStores.FirstOrDefault(s => s.StoreId == value.StoreId);
                }
            }
        }

        partial void OnSelectedStoreChanged(Store? value)
        {
            // TODO: Update user's selected store in backend/preferences
            // Access store ID: value?.StoreId
            // Access store name: value?.Name
        }

        [RelayCommand]
        private async Task Logout()
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Logout", 
                "Are you sure you want to logout?", 
                "Yes", 
                "No");

            if (confirm)
            {
                // TODO: Clear authentication tokens/session
                // Clear preferences/stored credentials
                // Preferences.Clear();
                
                // Navigate to login page
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }
    }    
}