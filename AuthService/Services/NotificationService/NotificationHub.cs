using Microsoft.AspNetCore.SignalR;

namespace AuthService.Services.NotificationService
{
    public class NotificationHub : Hub<INotificationClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveNotification($"thanks : {Context.ConnectionId}");
        }

        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).ReceiveNotification(message);
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
