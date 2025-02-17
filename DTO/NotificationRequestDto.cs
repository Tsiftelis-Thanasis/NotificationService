namespace NotificationService.DTO
{
    public class NotificationRequestDto
    {
        public string Recipient { get; set; } // Email, Phone number, etc.
        public string Message { get; set; }
        public string Type { get; set; } // Email, SMS, Push

        public NotificationRequestDto(string recipient, string message, string type)
        {
            Recipient = recipient;
            Message = message;
            Type = type;
        }
    }

}
