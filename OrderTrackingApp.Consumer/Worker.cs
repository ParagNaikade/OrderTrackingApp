using OrderTrackingApp.Application.Interfaces;
using OrderTrackingApp.Domain.Events;
using OrderTrackingApp.ReadPersistence.Interfaces;
using OrderTrackingApp.ReadPersistence.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace OrderTrackingApp.Consumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private IConnection? _connection;
    private IChannel? _channel;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await InitializeRabbitMqListener();

        var consumer = new AsyncEventingBasicConsumer(_channel!);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var orderWriteRepository = scope.ServiceProvider.GetRequiredService<IOrderWriteRepository>();
                var orderReadRepository = scope.ServiceProvider.GetRequiredService<IOrderReadRepository>();

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation("Received message: {message}", message);

                var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

                if (orderEvent != null)
                {
                    var order = await orderWriteRepository.GetByIdAsync(orderEvent.OrderId)
                                ?? throw new Exception($"Order with ID {orderEvent.OrderId} not found.");

                    var readModel = new OrderReadModel
                    {
                        Id = order.Id,
                        CustomerId = order.CustomerId,
                        Status = order.Status,
                        CreatedAt = order.OrderDate,
                        Items = order.Items.Select(i => new OrderItemReadModel
                        {
                            ProductId = i.ProductId,
                            Quantity = i.Quantity,
                            UnitPrice = i.UnitPrice
                        }).ToList()
                    };

                    await orderReadRepository.InsertAsync(readModel);

                    _logger.LogInformation("Synced OrderId {orderId} to MongoDB", order.Id);

                    await _channel!.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process RabbitMQ message.");

                await _channel!.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        await _channel!.BasicConsumeAsync(queue: "orders",
                                          autoAck: false,
                                          consumer: consumer,
                                          stoppingToken);
    }

    private async Task InitializeRabbitMqListener()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.BasicQosAsync(0, 1, false);

        await _channel.QueueDeclareAsync(queue: "orders",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
    }
}
