using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;

namespace Com.Natoma.Adpq.Prototype.Business.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<RequestResult> Get(int id);
        Task<RequestResult> Get(string email, string password);
        Task<RequestResult> Create(UserProfileViewModel userProfileViewModel);
        Task<RequestResult> Update(UserProfileViewModel userProfileViewModel);

    }
}
