using System.Threading.Tasks;

namespace Com.Natoma.Adpq.Prototype.Business.Services.Interfaces
{
    public interface ISmsService
    {
        Task<bool> SendSms(string phoneNumber, string smsMessage);
    }
}
