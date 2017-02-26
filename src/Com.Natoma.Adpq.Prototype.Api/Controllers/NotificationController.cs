using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Models.Notification;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Com.Natoma.Adpq.Prototype.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_notificationService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            return Ok(_notificationService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NotificationViewModel notificationViewModel)
        {
            return Ok(await _notificationService.CreateAndSendNotification(notificationViewModel));
        }
        
    }
}
