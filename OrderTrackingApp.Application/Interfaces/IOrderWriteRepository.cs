using OrderTrackingApp.Domain.Entities;

namespace OrderTrackingApp.Application.Interfaces
{
    public interface IOrderWriteRepository
    {
        Task AddAsync(Order order);

        Task UpdateAsync(Order order);
        
        Task DeleteAsync(Guid id);
    }
}
