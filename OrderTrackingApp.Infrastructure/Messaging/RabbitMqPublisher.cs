using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OrderTrackingApp.Infrastructure.Messaging
{
    internal class RabbitMqPublisher : IMessagePublisher
    {
        private readonly IConnection _connection;

        public RabbitMqPublisher(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RabbitMq");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("RabbitMQ connection string is not configured.");
            }

            var factory = new ConnectionFactory
            {
                Uri = new Uri(connectionString)
            };

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        }

        public async Task PublishAsync<T>(string topic, T message)
        {
            using var channel = await _connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: topic, durable: true, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            await channel.BasicPublishAsync(exchange: "", routingKey: topic, body: body);
        }

    }
}
