using AuthService.Services.NotificationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController2 : ControllerBase
    {
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

        public NotificationController2(IHubContext<NotificationHub,INotificationClient> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task <IActionResult> Post (Notification notification)
        {
            await _hubContext.Clients.All.ReceiveNotification($"-> {notification.Message}");
            return Ok();
        }
    }

    public class Notification
    {
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
