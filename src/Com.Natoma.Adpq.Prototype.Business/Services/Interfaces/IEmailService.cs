using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Natoma.Adpq.Prototype.Business.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, string subject, string message);
    }
}
