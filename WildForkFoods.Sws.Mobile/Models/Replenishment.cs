using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WildForkFoods.Sws.Mobile.Models
{
    public partial class ReplenishmentLine : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string sku;

        [ObservableProperty]
        private int quantity;

        [ObservableProperty]
        private string unit;

        [ObservableProperty]
        private string? notes;

        public ReplenishmentLine(string name, string sku, int quantity, string unit, string? notes = null)
        {
            this.name = name;
            this.sku = sku;
            this.quantity = quantity;
            this.unit = unit;
            this.notes = notes;
        }
    }

    public record ReplenishmentOrder(
        string StoreId,
        string UserId,
        DateTime CreatedUtc,
        List<ReplenishmentLine> Lines,
        string? Reference = null);
}
