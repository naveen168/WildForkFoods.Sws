using WildForkFoods.Sws.Mobile.ViewModels;
using ZXing.Net.Maui;

namespace WildForkFoods.Sws.Mobile.Views;

public partial class ScanPage : ContentPage
{
    private readonly ScanViewModel _vm;
    public ScanPage(ScanViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    private void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        var upc = e.Results?.FirstOrDefault()?.Value;
        if (!string.IsNullOrWhiteSpace(upc))
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _vm.OnBarcodeAsync(upc);
            });
        }
    }
}