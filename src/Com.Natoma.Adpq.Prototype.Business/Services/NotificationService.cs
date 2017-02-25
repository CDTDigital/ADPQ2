using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Models.Notification;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Com.Natoma.Adpq.Prototype.Business.Services
{
    public class NotificationService: INotificationService
    {
        private readonly adpq2adpqContext _context;

        public NotificationService(adpq2adpqContext context)
        {
            _context = context;
        }

        public async Task<RequestResult> Get()
        {
            return null;
        }

        public async Task<RequestResult> Get(int userProfileId)
        {
            return null;
        }

        public Task<RequestResult> SendNotification()
        {
            throw new System.NotImplementedException();
        }

        private List<User> GetUserToNotify(int notificatinId)
        {
            return null;
        }
    }
}
