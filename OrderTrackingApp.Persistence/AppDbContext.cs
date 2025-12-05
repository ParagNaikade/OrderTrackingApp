using Microsoft.EntityFrameworkCore;
using OrderTrackingApp.Domain.Entities;

namespace OrderTrackingApp.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders => Set<Order>();

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.OrderNumber)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(x => x.Status)
                      .HasConversion<string>(); // Enum -> string

                entity.Property(x => x.OrderDate)
                      .IsRequired();

                // OrderItem - owned collection
                entity.OwnsMany(x => x.Items, items =>
                {
                    items.WithOwner().HasForeignKey("OrderId");
                    items.HasKey("OrderId", "ProductId"); // Composite Key

                    items.Property(x => x.ProductId).IsRequired();
                    items.Property(x => x.Quantity).IsRequired();
                    items.Property(x => x.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
                    items.Ignore(x => x.TotalPrice); // Computed property
                    items.ToTable("OrderItems");
                });
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(x => x.Sku)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(x => x.Description)
                      .HasMaxLength(500);

                entity.Property(x => x.Price)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.Property(x => x.StockQuantity)
                      .IsRequired();
            });
        }
    }
}
