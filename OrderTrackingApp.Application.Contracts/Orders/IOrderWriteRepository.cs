namespace OrderTrackingApp.Application.Contracts.Orders
{
    public interface IOrderWriteRepository
    {
        Task AddAsync(OrderDto order);

        Task UpdateAsync(OrderDto order);
        
        Task DeleteAsync(Guid id);

        Task<OrderDto?> GetByIdAsync(Guid id);
    }
}
