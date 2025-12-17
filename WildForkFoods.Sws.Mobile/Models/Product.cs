using CommunityToolkit.Mvvm.ComponentModel;

namespace WildForkFoods.Sws.Mobile.Models
{
    public record Product(
       string Sku,
       string Upc,
       string Name,
       string Unit,     // "EA"
       decimal Cost,
       int CasePack,
       int CurrentOnHand,
       DateTime? LastOrderedAt);

    public partial class ProductViewModel : ObservableObject
    {
        public Product Product { get; }

        [ObservableProperty]
        private bool isAdded;

        [ObservableProperty]
        private string buttonText = "Add";

        public string Sku => Product.Sku;
        public string Upc => Product.Upc;
        public string Name => Product.Name;
        public string Unit => Product.Unit;
        public decimal Cost => Product.Cost;
        public int CasePack => Product.CasePack;
        public int CurrentOnHand => Product.CurrentOnHand;
        public DateTime? LastOrderedAt => Product.LastOrderedAt;

        public ProductViewModel(Product product)
        {
            Product = product;
        }

        public void MarkAsAdded()
        {
            IsAdded = true;
            ButtonText = "Added";
        }
    }
}
