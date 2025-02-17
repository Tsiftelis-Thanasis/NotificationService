namespace NotificationService.Workers
{
    using Microsoft.Extensions.Hosting;
    using NotificationService.DTO;
    using NotificationService.Messaging;
    using System.Text.Json;

    public class NotificationWorker : BackgroundService
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationWorker> _logger;

        public NotificationWorker(IRabbitMqService rabbitMqService, INotificationService notificationService, ILogger<NotificationWorker> logger)
        {
            _rabbitMqService = rabbitMqService;
            _notificationService = notificationService;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _rabbitMqService.ConsumeAsync("notification_queue", async (message) =>
            {
                var request = JsonSerializer.Deserialize<NotificationRequestDto>(message);
                await _notificationService.SendNotificationAsync(request);
            });

            return Task.CompletedTask;
        }
    }

}
