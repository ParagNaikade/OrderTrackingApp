namespace OrderTrackingApp.Application.Contracts.Orders
{
    public interface IOrderReadRepository
    {
        Task<OrderDto?> GetByIdAsync(Guid id);

        Task<List<OrderDto>> GetAllAsync();

        Task InsertAsync(OrderDto order);
    }
}
