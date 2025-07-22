using Microsoft.EntityFrameworkCore;
using OrderTrackingApp.Domain.Entities;
using OrderTrackingApp.Persistence;

namespace OrderTrackingApp.MigrationRunner
{
    internal static class ProductSeeder
    {
        public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
        {
            if (await context.Products.AnyAsync(cancellationToken))
            {
                return; // Already seeded
            }

            var products = new List<Product>
            {
                new()
                {
                    Id = Guid.Parse("b111a4dd-f45c-4d40-a6a3-3002f62f823f"),
                    Name = "Wireless Mouse",
                    Sku = "MSE-001",
                    Description = "Ergonomic wireless mouse",
                    Price = 29.99m,
                    StockQuantity = 1000
                },
                new()
                {
                    Id = Guid.Parse("2a13551b-059c-4035-b275-8a8cb82bd272"),
                    Name = "Mechanical Keyboard",
                    Sku = "KEY-101",
                    Description = "RGB mechanical keyboard",
                    Price = 79.99m,
                    StockQuantity = 500
                }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
