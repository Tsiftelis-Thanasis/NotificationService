using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NotificationService.DTO;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public async Task SendNotificationAsync(NotificationRequestDto notificationRequestDto )
        {
            _logger.LogInformation($"Sending notification: {notificationRequestDto.Message}");
            await Task.CompletedTask;
        }
    }
}

