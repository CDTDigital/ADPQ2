using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Models.Message;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Com.Natoma.Adpq.Prototype.Business.Services
{
    public class MessageService: IMessageService
    {
        private readonly adpq2adpqContext _context;

        public MessageService(adpq2adpqContext context)
        {
            _context = context;
        }

        public List<MessageViewModel> Get()
        {
            return null;
        }

    }
}
