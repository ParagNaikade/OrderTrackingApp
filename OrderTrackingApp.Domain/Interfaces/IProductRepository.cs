using OrderTrackingApp.Domain.Entities;

namespace OrderTrackingApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);

        Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task UpdateProducts(List<Product> updatedProducts);
    }
}
