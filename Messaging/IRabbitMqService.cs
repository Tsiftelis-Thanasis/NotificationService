namespace NotificationService.Messaging
{
    public interface IRabbitMqService
    {
        Task PublishAsync(string queue, string message);
        Task ConsumeAsync(string queue, Action<string> onMessageReceived);
    }

}
