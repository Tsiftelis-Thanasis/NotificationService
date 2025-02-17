namespace NotificationService.Data
{
    public class Notification
    {
        public int Id { get; set; }
        public string Recipient { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // Email, SMS, Push
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // Pending, Sent, Failed
    }

}
