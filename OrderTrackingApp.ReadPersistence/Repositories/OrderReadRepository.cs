using MongoDB.Driver;
using OrderTrackingApp.ReadPersistence.Interfaces;
using OrderTrackingApp.ReadPersistence.Models;

namespace OrderTrackingApp.ReadPersistence.Repositories
{
    public class OrderReadRepository(MongoDbContext context) : IOrderReadRepository
    {
        private readonly IMongoCollection<OrderReadModel> _collection = context.Database.GetCollection<OrderReadModel>("Orders");

        public async Task<List<OrderReadModel>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<OrderReadModel?> GetByIdAsync(Guid id)
        {
            return await _collection.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(OrderReadModel order)
        {
            await _collection.InsertOneAsync(order);
        }
    }
}
