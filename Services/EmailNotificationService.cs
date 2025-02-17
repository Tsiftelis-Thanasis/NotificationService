using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class EmailNotificationService : INotificationService
{
    private readonly NotificationDbContext _context;
    private readonly ILogger<EmailNotificationService> _logger;

    public EmailNotificationService(NotificationDbContext context, ILogger<EmailNotificationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SendNotificationAsync(NotificationRequestDto request)
    {
        var notification = new Notification
        {
            Recipient = request.Recipient,
            Message = request.Message,
            Type = "Email",
            Status = "Sent"
        };

        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Email sent to {request.Recipient}");
    }
}
