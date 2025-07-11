using MongoDB.Driver;
using OrderTrackingApp.Application.DTOs;
using OrderTrackingApp.ReadPersistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingApp.ReadPersistence.Repositories
{
    internal class OrderReadRepository : IOrderReadRepository
    {
        private readonly IMongoCollection<OrderDto> _collection;

        public OrderReadRepository(MongoDbContext context)
        {
            _collection = context.Database.GetCollection<OrderDto>("Orders");
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            return await _collection.Find(o => o.Id == id).FirstOrDefaultAsync();
        }
    }
}
