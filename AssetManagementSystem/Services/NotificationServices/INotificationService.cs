using AssetManagementSystem.Models.Domains;
using AssetManagementSystem.Models.Dtos;

namespace AssetManagementSystem.Services.NotificationServices
{
    public interface INotificationService
    {
        Task<Notification> AddNotificationAsync(NotificationDto notificationDto);
        Task<List<Notification>> GetAllNotificationAsync();
        Task<Notification?> DeleteNotificationAsync(int id);
    }
}
