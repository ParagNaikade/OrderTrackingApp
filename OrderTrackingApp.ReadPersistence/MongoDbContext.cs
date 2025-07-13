using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace OrderTrackingApp.ReadPersistence
{
    internal class MongoDbContext
    {
        public IMongoDatabase Database { get; }

        public MongoDbContext(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("MongoDb");
            var mongoClient = new MongoClient(connectionString);
            Database = mongoClient.GetDatabase("OrderReadDb");
        }
    }
}
