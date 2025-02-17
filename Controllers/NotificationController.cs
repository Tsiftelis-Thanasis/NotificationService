using Microsoft.AspNetCore.Mvc;
using NotificationService.DTO;
using NotificationService.Messaging;
using System.Text.Json;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IRabbitMqService _rabbitMqService;

        public NotificationController(INotificationService notificationService, IRabbitMqService rabbitMqService)
        {
            _notificationService = notificationService;
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public IActionResult SendNotification([FromBody] NotificationRequestDto request)
        {
            _rabbitMqService.PublishAsync("notification_queue", JsonSerializer.Serialize(request));
            return Accepted(new { message = "Notification request queued" });
        }

        [HttpGet("consume")]
        public IActionResult StartConsuming()
        {
            _rabbitMqService.ConsumeAsync("notificationQueue", msg =>
            {
                Console.WriteLine($"Received Notification: {msg}");
            });

            return Ok("Consumer started.");
        }

    }

}
