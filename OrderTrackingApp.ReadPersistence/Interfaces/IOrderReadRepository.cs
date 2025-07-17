using OrderTrackingApp.ReadPersistence.Models;

namespace OrderTrackingApp.ReadPersistence.Interfaces
{
    public interface IOrderReadRepository
    {
        Task<OrderReadModel?> GetByIdAsync(Guid id);

        Task<List<OrderReadModel>> GetAllAsync();

        Task InsertAsync(OrderReadModel order);
    }
}
