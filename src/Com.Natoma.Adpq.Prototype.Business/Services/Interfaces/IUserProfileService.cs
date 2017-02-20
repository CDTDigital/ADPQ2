using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;

namespace Com.Natoma.Adpq.Prototype.Business.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileViewModel> Get(int id);
        Task<UserProfileViewModel> Create(UserProfileViewModel userProfileViewModel);

    }
}
