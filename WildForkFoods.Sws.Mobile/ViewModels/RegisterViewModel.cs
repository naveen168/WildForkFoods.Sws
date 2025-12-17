using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WildForkFoods.Sws.Mobile.Models;
using WildForkFoods.Sws.Mobile.Services;
using WildForkFoods.Sws.Mobile.Views;

namespace WildForkFoods.Sws.Mobile.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly UserService _userService;

        [ObservableProperty]
        private string userName = string.Empty;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string confirmPassword = string.Empty;

        [ObservableProperty]
        private ObservableCollection<Store> availableStores = new();

        [ObservableProperty]
        private Store? selectedStore;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private bool isLoading = false;

        public RegisterViewModel(UserService userService)
        {
            _userService = userService;
            LoadStores();
        }

        private void LoadStores()
        {
            // Load available stores
            AvailableStores = new ObservableCollection<Store>
            {
                new Store { StoreId = "1094", Name = "Sobeys Reenders" },
                new Store { StoreId = "1099", Name = "Sobeys St. Vital" },
                new Store { StoreId = "1100", Name = "Sobeys Main Street" }
            };
        }

        [RelayCommand]
        private async Task Register()
        {
            // Clear previous error
            ErrorMessage = string.Empty;

            // Validate inputs
            if (!ValidateInputs())
                return;

            IsLoading = true;

            try
            {
                // TODO: Call API to register user
                await Task.Delay(1000); // Simulate API call

                // Create new user
                var newUser = new User
                {
                    UserName = UserName.Trim(),
                    Name = Name.Trim(),
                    Email = Email.Trim(),
                    Role = "Store Associate", // Default role
                    StoreId = SelectedStore?.StoreId ?? string.Empty
                };

                // TODO: Save user via UserService/API
                // await _userService.RegisterAsync(newUser, Password);

                // Show success message
                await Shell.Current.DisplayAlert(
                    "Success",
                    "Account created successfully! Please login.",
                    "OK");

                // Navigate to login page
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Registration failed: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                ErrorMessage = "Please enter a username";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Please enter your full name";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Please enter your email";
                return false;
            }

            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Please enter a valid email address";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter a password";
                return false;
            }

            if (Password.Length < 6)
            {
                ErrorMessage = "Password must be at least 6 characters";
                return false;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match";
                return false;
            }

            if (SelectedStore == null)
            {
                ErrorMessage = "Please select a store";
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [RelayCommand]
        private async Task NavigateToLogin()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}