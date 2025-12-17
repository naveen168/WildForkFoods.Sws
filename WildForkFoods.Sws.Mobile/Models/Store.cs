namespace WildForkFoods.Sws.Mobile.Models;

public class Store
{
    public string StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DisplayName => $"{StoreId} - {Name}";
}