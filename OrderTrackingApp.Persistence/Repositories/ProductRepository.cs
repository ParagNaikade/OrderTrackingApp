using Microsoft.EntityFrameworkCore;
using OrderTrackingApp.Domain.Entities;
using OrderTrackingApp.Domain.Interfaces;

namespace OrderTrackingApp.Persistence.Repositories
{
    public class ProductRepository(AppDbContext appDbContext) : IProductRepository
    {
        public Task AddAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return appDbContext.Products.Where(p => ids.Any(id => id == p.Id)).ToListAsync();
        }

        public Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProducts(List<Product> updatedProducts)
        {
            throw new NotImplementedException();
        }
    }
}
