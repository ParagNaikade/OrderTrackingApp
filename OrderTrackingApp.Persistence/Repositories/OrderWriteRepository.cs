using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderTrackingApp.Application.Contracts.Orders;
using OrderTrackingApp.Domain.Entities;

namespace OrderTrackingApp.Persistence.Repositories
{
    public class OrderWriteRepository(AppDbContext dbContext, IMapper mapper) : IOrderWriteRepository
    {
        public async Task AddAsync(OrderDto orderDto)
        {
            var order = mapper.Map<Order>(orderDto);

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

        public async Task UpdateAsync(OrderDto orderDto)
        {
            var order = mapper.Map<Order>(orderDto);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            var order = await dbContext.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(x => x.Id == id);

            return mapper.Map<OrderDto?>(order);
        }
    }
}
