using Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KhumaloCrafts.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly IDurableFunctionsService _durableFunctionsService;

        public NotificationsController(IDurableFunctionsService durableFunctionsService)
        {
            _durableFunctionsService = durableFunctionsService;
        }

        public async Task<IActionResult> Notifications()
        {
            // Use a placeholder instanceId; replace it with the actual one from your business logic
            string instanceId = "some-instance-id";
            var notifications = await _durableFunctionsService.GetNotificationsAsync(instanceId);
            return View(notifications);
        }

        public async Task<IActionResult> Details(string instanceId)
        {
            var notificationDetails = await _durableFunctionsService.GetNotificationDetailsAsync(instanceId);
            return View(notificationDetails);
        }
    }
}
