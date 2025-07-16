using OrderTrackingApp.Application.Interfaces;
using OrderTrackingApp.Domain.Events;
using OrderTrackingApp.ReadPersistence.Interfaces;
using OrderTrackingApp.ReadPersistence.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace OrderTrackingApp.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private IConnection _connection;
        private IChannel _channel;

        public Worker(ILogger<Worker> logger, IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _logger = logger;
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;

            InitializeRabbitMqListener().GetAwaiter().GetResult();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"Received message: {message}");

                var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

                if (orderEvent != null)
                {
                    var order = await _orderWriteRepository.GetByIdAsync(orderEvent.OrderId) ?? throw new Exception($"Order with ID {orderEvent.OrderId} not found.");

                    var readModel = new OrderReadModel
                    {
                        Id = order.Id,
                        CustomerId = order.CustomerId,
                        Status = order.Status,
                        CreatedAt = order.OrderDate,
                        Items = [.. order.Items.Select(i => new OrderItemReadModel
                                    {
                                        ProductId = i.ProductId,
                                        Quantity = i.Quantity,
                                        UnitPrice = i.UnitPrice
                                    })]
                    };

                    await _orderReadRepository.InsertAsync(readModel);

                    _logger.LogInformation($"Synced OrderId {order.Id} to MongoDB");
                }
            };

            await _channel.BasicConsumeAsync(queue: "orders",
                                    autoAck: true,
                                    consumer: consumer);
        }

        private async Task InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(queue: "orders",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }
    }
}
