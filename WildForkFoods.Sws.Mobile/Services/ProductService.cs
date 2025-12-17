using System;
using System.Collections.Generic;
using System.Text;
using WildForkFoods.Sws.Mobile.Models;

namespace WildForkFoods.Sws.Mobile.Services
{
    public class ProductService     
    {
        // Hardcoded products for demo - Wild Fork Foods premium meat & seafood
        private readonly List<Product> _products = new()
        {
            // Beef Products
            new("BEEF-001", "074700001234", "USDA Prime Ribeye Steak", "LB", 25.99m, 12, 24, DateTime.UtcNow.AddDays(-5)),
            new("BEEF-105", "074700001241", "Wagyu Beef Burgers", "LB", 14.99m, 20, 18, DateTime.UtcNow.AddDays(-3)),
            new("BEEF-220", "074700001258", "Filet Mignon", "LB", 32.99m, 10, 15, DateTime.UtcNow.AddDays(-7)),
            new("BEEF-325", "074700001265", "New York Strip Steak", "LB", 21.99m, 15, 22, DateTime.UtcNow.AddDays(-2)),
            new("BEEF-430", "074700001272", "Beef Brisket", "EA", 45.99m, 8, 10, null),
            new("BEEF-535", "074700001289", "Ground Beef 80/20", "LB", 7.99m, 30, 45, DateTime.UtcNow.AddDays(-1)),
            new("BEEF-640", "074700001296", "Bone-In Short Ribs", "LB", 18.99m, 12, 20, null),
            
            // Chicken Products
            new("CHKN-102", "074700002234", "Organic Free-Range Chicken Breast", "LB", 12.99m, 24, 36, DateTime.UtcNow.AddDays(-4)),
            new("CHKN-201", "074700002241", "Chicken Drumsticks", "LB", 6.99m, 30, 42, DateTime.UtcNow.AddDays(-6)),
            new("CHKN-305", "074700002258", "Chicken Wings", "LB", 8.99m, 25, 50, DateTime.UtcNow.AddDays(-1)),
            new("CHKN-420", "074700002265", "Whole Chicken", "EA", 12.99m, 15, 18, null),
            
            // Seafood Produc001s
            new("SEAFOOD-205", "074700003234", "Wild Caught Atlantic Salmon", "LB", 18.99m, 18, 25, DateTime.UtcNow.AddDays(-3)),
            new("SEAFOOD-310", "074700003241", "Jumbo Argentinian Red Shrimp", "LB", 19.99m, 20, 30, DateTime.UtcNow.AddDays(-5)),
            new("SEAFOOD-415", "074700003258", "Ahi Tuna Steaks", "LB", 24.99m, 12, 15, null),
            new("SEAFOOD-520", "074700003265", "Sea Scallops", "LB", 28.99m, 10, 12, DateTime.UtcNow.AddDays(-8)),
            new("SEAFOOD-625", "074700003272", "Lobster Tails", "EA", 35.99m, 8, 10, null),
            new("SEAFOOD-730", "074700003289", "Mahi Mahi Fillets", "LB", 19.99m, 15, 20, DateTime.UtcNow.AddDays(-2)),
            new("SEAFOOD-810", "074700003296", "King Crab Legs", "LB", 45.99m, 6, 8, null),
            
            // Pork Products
            new("PORK-304", "074700004234", "Heritage Pork Tenderloin", "LB", 15.50m, 18, 24, DateTime.UtcNow.AddDays(-4)),
            new("PORK-505", "074700004241", "Pork Baby Back Ribs", "LB", 16.99m, 15, 22, DateTime.UtcNow.AddDays(-6)),
            new("PORK-610", "074700004258", "Pork Chops", "LB", 9.99m, 25, 35, DateTime.UtcNow.AddDays(-1)),
            new("PORK-715", "074700004265", "Italian Sausage Links", "LB", 8.99m, 30, 40, DateTime.UtcNow.AddDays(-3)),
            
            // Lamb Products
            new("LAMB-401", "074700005234", "New Zealand Lamb Chops", "LB", 22.99m, 12, 16, DateTime.UtcNow.AddDays(-7)),
            new("LAMB-510", "074700005241", "Ground Lamb", "LB", 11.99m, 20, 28, null),
            
            // Specialty Items
            new("BEEF-750", "074700001303", "Kobe Beef Sirloin", "LB", 89.99m, 5, 8, null),
            new("SEAFOOD-920", "074700003303", "Chilean Sea Bass", "LB", 34.99m, 8, 12, DateTime.UtcNow.AddDays(-4)),
            new("PORK-820", "074700004272", "Berkshire Pork Belly", "LB", 13.99m, 15, 20, null),
            new("CHKN-530", "074700002272", "Duck Breast", "LB", 24.99m, 10, 12, null),
            new("LAMB-620", "074700005248", "Rack of Lamb", "EA", 42.99m, 8, 10, DateTime.UtcNow.AddDays(-5)),
            new("SEAFOOD-1025", "074700003310", "Swordfish Steaks", "LB", 26.99m, 12, 15, DateTime.UtcNow.AddDays(-2)),
        };

        public Task<List<Product>> SearchAsync(string query)
        {
            query = (query ?? "").Trim().ToLowerInvariant();
            var res = _products
                .Where(p => p.Name.ToLowerInvariant().Contains(query)
                         || p.Sku.ToLowerInvariant().Contains(query)
                         || p.Upc.ToLowerInvariant().Contains(query))
                .OrderBy(p => p.Name)
                .ToList();
            return Task.FromResult(res);
        }

        public Task<Product?> GetByUpcAsync(string upc)
        {
            var p = _products.FirstOrDefault(x => x.Upc == upc);
            return Task.FromResult(p);
        }

        public Task<Product?> GetBySkuAsync(string sku)
        {
            var p = _products.FirstOrDefault(x => x.Sku == sku);
            return Task.FromResult(p);
        }

        public Task<List<Product>> GetAllAsync() => Task.FromResult(_products.ToList());
    }
}
