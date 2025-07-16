using Microsoft.EntityFrameworkCore;
using OrderTrackingApp.Domain.Entities;

namespace OrderTrackingApp.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Status)
                      .HasConversion<string>(); // maps OrderStatus enum to string
            });
        }
    }
}
