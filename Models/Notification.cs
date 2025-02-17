namespace NotificationService.Models
{
        public class Notification
        {
            public string Id { get; set; } = Guid.NewGuid().ToString();
            public string Message { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }
    }
