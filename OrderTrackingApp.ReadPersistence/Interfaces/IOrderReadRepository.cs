using OrderTrackingApp.Application.DTOs;

namespace OrderTrackingApp.ReadPersistence.Interfaces
{
    public interface IOrderReadRepository
    {
        Task<OrderDto?> GetByIdAsync(Guid id);

        Task<List<OrderDto>> GetAllAsync();
    }
}
