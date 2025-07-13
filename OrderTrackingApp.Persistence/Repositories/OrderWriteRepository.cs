using Microsoft.EntityFrameworkCore;
using OrderTrackingApp.Application.Interfaces;
using OrderTrackingApp.Domain.Entities;

namespace OrderTrackingApp.Persistence.Repositories
{
    internal class OrderWriteRepository(AppDbContext dbContext) : IOrderWriteRepository
    {
        public async Task AddAsync(Order order)
        {
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order =  await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if(order != null)
            {
                dbContext.Orders.Remove(order);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Order order)
        {
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync();
        }
    }
}
