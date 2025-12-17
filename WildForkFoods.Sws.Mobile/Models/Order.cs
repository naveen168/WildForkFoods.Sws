using System.Collections.ObjectModel;

namespace WildForkFoods.Sws.Mobile.Models
{
    public class Order
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public int ItemCount { get; set; }
        public decimal TotalAmount { get; set; }
        public ObservableCollection<OrderLine> OrderLines { get; set; } = new();

    }

    public class OrderLine
    {
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
    }
}