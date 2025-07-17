using Microsoft.EntityFrameworkCore;
using OrderTrackingApp.Persistence.Repositories;

namespace OrderTrackingApp.Persistence.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var dbContext = CreateDbContext();

            var repo= new OrderWriteRepository(dbContext);

            var id = Guid.NewGuid();
            await repo.AddAsync(new Domain.Entities.Order
            {
                Id = id,
                CustomerId = Guid.NewGuid(),
                OrderDate = DateTime.UtcNow,
                Status = Domain.Entities.OrderStatus.Pending,
                Items = new List<Domain.Entities.OrderItem>
                {
                    new Domain.Entities.OrderItem
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                        UnitPrice = 10.0m
                    }
                }
            });

            var order = await repo.GetByIdAsync(id);
        }

        private AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=localhost,1433;Database=OrderDb;User=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True")
                .Options;
            return new AppDbContext(options);
        }
    }
}