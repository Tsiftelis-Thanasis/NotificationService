using System;
using System.Text;
using System.Threading.Tasks;
using NotificationService.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class RabbitMqService: IRabbitMqService
{
    private readonly ConnectionFactory _factory;

    public RabbitMqService()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };
    }

    public async Task PublishAsync(string queue, string message)
    {
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await  channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);

        var body = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync("", queue, body);
    }

    public async Task ConsumeAsync(string queue, Action<string> onMessageReceived)
    {
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);

      
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");
            return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync("", autoAck: true, consumer: consumer);



    }
}
