using OrderTrackingApp.Application.Contracts.Orders;
using OrderTrackingApp.Domain.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace OrderTrackingApp.Consumer;

public class Worker(ILogger<Worker> logger, IServiceProvider serviceProvider) : BackgroundService
{
    private IConnection? _connection;
    private IChannel? _channel;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await InitializeRabbitMqListener();

        var consumer = new AsyncEventingBasicConsumer(_channel!);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var orderWriteRepository = scope.ServiceProvider.GetRequiredService<IOrderWriteRepository>();
                var orderReadRepository = scope.ServiceProvider.GetRequiredService<IOrderReadRepository>();

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                logger.LogInformation("Received message: {message}", message);

                var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

                if (orderEvent != null)
                {
                    var order = await orderWriteRepository.GetByIdAsync(orderEvent.OrderId)
                                ?? throw new Exception($"Order with ID {orderEvent.OrderId} not found.");

                    await orderReadRepository.InsertAsync(order);

                    logger.LogInformation("Synced OrderId {orderId} to MongoDB", order.Id);

                    await _channel!.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process RabbitMQ message.");

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
