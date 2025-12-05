using AutoMapper;
using MongoDB.Driver;
using OrderTrackingApp.Application.Contracts;
using OrderTrackingApp.Application.Contracts.Orders;
using OrderTrackingApp.Domain.Entities;
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

        public async Task<PaginatedResult<OrderDto>> GetPaginatedOrdersAsync(int page, int pageSize, string? status,
                                                                       DateTime? fromDate, DateTime? toDate,
                                                                       CancellationToken cancellationToken)
        {
            var filterBuilder = Builders<OrderReadModel>.Filter;
            var filters = new List<FilterDefinition<OrderReadModel>>();

            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<OrderStatus>(status, out var parsedStatus))
            {
                filters.Add(filterBuilder.Eq(o => o.Status, parsedStatus));
            }

            if (fromDate.HasValue)
            {
                filters.Add(filterBuilder.Gte(o => o.CreatedAt, fromDate.Value));
            }
                
            if (toDate.HasValue)
            {
                filters.Add(filterBuilder.Lte(o => o.CreatedAt, toDate.Value));
            }

            var filter = filters.Any() ? filterBuilder.And(filters) : FilterDefinition<OrderReadModel>.Empty;

            var totalCount = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

            var orders = await _collection.Find(filter)
                .SortByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync(cancellationToken);

            var dtos = mapper.Map<List<OrderDto>>(orders);

            return new PaginatedResult<OrderDto>(dtos, totalCount, page, pageSize);
        }

        public async Task InsertAsync(OrderDto orderDto)
        {
            var order = mapper.Map<OrderReadModel>(orderDto);

            await _collection.InsertOneAsync(order);
        }
    }
}
