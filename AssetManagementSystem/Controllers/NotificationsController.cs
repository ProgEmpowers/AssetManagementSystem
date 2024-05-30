using AssetManagementSystem.CustomActionFilters;
using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Services.NotificationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody] NotificationDto notificationDto)
        {
            if (notificationDto == null)
                return BadRequest();

            var addedNotification = await notificationService.AddNotificationAsync(notificationDto);
            return Ok(addedNotification);
        }

        [HttpGet]
        public async Task<IActionResult> GetallNotifications()
        {
            var AllNotifications = await notificationService.GetAllNotificationAsync();
            return Ok(AllNotifications);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteNotification([FromRoute] int id)
        {
            var SelectedNotification = await notificationService.DeleteNotificationAsync(id);
            if (SelectedNotification == null)
            {
                return NotFound();
            }
            return Ok(SelectedNotification);
        }
    }
}
