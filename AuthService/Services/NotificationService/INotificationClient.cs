namespace AuthService.Services.NotificationService
{
    public interface INotificationClient
    {
        Task ReceiveNotification(string message);
    }
}
