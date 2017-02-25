using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;

namespace Com.Natoma.Adpq.Prototype.Business.Services.Interfaces
{
    public interface INotificationService
    {
        Task<RequestResult> Get();
        Task<RequestResult> Get(int userProfileId);
        Task<RequestResult> SendNotification();
    }
}
