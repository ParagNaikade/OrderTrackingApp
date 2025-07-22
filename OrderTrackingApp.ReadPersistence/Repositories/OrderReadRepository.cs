using AutoMapper;
using MongoDB.Driver;
using OrderTrackingApp.Application.Contracts.Orders;
using OrderTrackingApp.ReadPersistence.Models;

namespace OrderTrackingApp.ReadPersistence.Repositories
{
    public class OrderReadRepository(MongoDbContext context, IMapper mapper) : IOrderReadRepository
    {
        private readonly IMongoCollection<OrderReadModel> _collection = context.Database.GetCollection<OrderReadModel>("Orders");

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _collection.Find(_ => true).ToListAsync();

            return mapper.Map<List<OrderDto>>(orders); 
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            var order = await _collection.Find(o => o.Id == id).FirstOrDefaultAsync();

            return mapper.Map<OrderDto?>(order);
        }

        public async Task InsertAsync(OrderDto orderDto)
        {
            var order = mapper.Map<OrderReadModel>(orderDto);

            await _collection.InsertOneAsync(order);
        }
    }
}
