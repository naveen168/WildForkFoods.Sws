using System;
using System.Collections.Generic;
using System.Text;
using WildForkFoods.Sws.Mobile.Models;

namespace WildForkFoods.Sws.Mobile.Services
{
    public class CartService
    {
        private readonly List<ReplenishmentLine> _lines = new();
        public IReadOnlyList<ReplenishmentLine> Lines => _lines;

        public void Add(string name, string sku, string unit, int qty, string? notes = null)
        {
            if (qty <= 0) return;
            var existing = _lines.FirstOrDefault(l => l.Sku == sku && l.Unit == unit);
            if (existing is null)
                _lines.Add(new ReplenishmentLine(name, sku, qty, unit, notes));
        }

        public void Update(string sku, string unit, int qty)
        {
            var existing = _lines.FirstOrDefault(l => l.Sku == sku && l.Unit == unit);
            if (existing is null) return;
            existing.Quantity = Math.Max(0, qty);
            if (existing.Quantity == 0) _lines.Remove(existing);
        }

        public void Remove(string sku, string unit) => _lines.RemoveAll(l => l.Sku == sku && l.Unit == unit);
        public void Clear() => _lines.Clear();
    }
}
