using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WildForkFoods.Sws.Mobile.Services;
using WildForkFoods.Sws.Mobile.Views;

namespace WildForkFoods.Sws.Mobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string userName = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private bool isLoading = false;

        public UserService _userService { get; set; }
        public LoginViewModel(UserService userService)
        {
            _userService = userService;
        }

        [RelayCommand]
        private async Task Login()
        {
            // Clear previous error
            ErrorMessage = string.Empty;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(UserName))
            {
                ErrorMessage = "Please enter your username";
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter your password";
                return;
            }

            IsLoading = true;

            try
            {
                // TODO: Replace with actual authentication service call
                await Task.Delay(1000); // Simulate API call

                // Demo authentication - accept any username/password
                // In production, validate against backend API
                var user = await _userService.GetByUserNameAsync(UserName);
                if (user == null)
                {
                    ErrorMessage = "User not found";
                    return;
                }
                else
                {
                    Preferences.Set("UserRole", user.Role);
                    // TODO: Store authentication token/session
                    // Navigate to main app and pass user object
                    var navigationParameter = new Dictionary<string, object>
                    {
                        { "User", user }
                    };
                    await Shell.Current.GoToAsync($"//{nameof(UserPage)}", navigationParameter);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Login failed: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task NavigateToRegister()
        {
            //await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        [RelayCommand]
        private void ClearError()
        {
            ErrorMessage = string.Empty;
        }
    }
}