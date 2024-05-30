using AssetManagementSystem.Context;
using AssetManagementSystem.Models.Domains;
using AssetManagementSystem.Models.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AssetManagementSystem.Services.NotificationServices
{
    public class NotificationService : INotificationService
    {
        private readonly AssetManagementDbContext _dbContext;
        private readonly IMapper mapper;

        public NotificationService(AssetManagementDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<Notification> AddNotificationAsync(NotificationDto notificationDto)
        {
            var NewNotification = mapper.Map<Notification>(notificationDto);
            await _dbContext.Notification.AddAsync(NewNotification);
            await _dbContext.SaveChangesAsync();
            return NewNotification;
        }

        public async Task<Notification?> DeleteNotificationAsync(int id)
        {
            var SelectedNotification = await _dbContext.Notification.FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedNotification == null)
            {
                return null;
            }
            _dbContext.Remove(SelectedNotification);
            return SelectedNotification;
        }

        public async Task<List<Notification>> GetAllNotificationAsync()
        {
            var Notifications = await _dbContext.Notification.ToListAsync();
            return Notifications;
        }
    }
}
