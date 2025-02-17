using NotificationService.DTO;

public interface INotificationService
{
    Task SendNotificationAsync(NotificationRequestDto request);
}
