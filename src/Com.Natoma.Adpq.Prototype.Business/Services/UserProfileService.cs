using System.Linq;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Com.Natoma.Adpq.Prototype.Business.Services
{
    public class UserProfileService: IUserProfileService
    {
        private readonly ADPQContext _context;

        public UserProfileService(ADPQContext context)
        {
            _context = context;
        }

        public async Task<UserProfileViewModel> Get(int id)
        {
            var result = await _context.UserProfile.Where(x => x.UserProfileId == id).FirstOrDefaultAsync();
            return result == null ? null : PopulateUserProfileViewModel(result) ;
        }

        public async Task<UserProfileViewModel> Create(UserProfileViewModel userProfileViewModel)
        {
            var newProfile = new UserProfile
            {
                AddressLine1 = userProfileViewModel.AddressLine1,
                AddressLine2 = userProfileViewModel.AddressLine2,
                City = userProfileViewModel.City,
                Email = userProfileViewModel.Email,
                FirstName = userProfileViewModel.FirstName,
                MiddleName = userProfileViewModel.MiddleName,
                LastName = userProfileViewModel.LastName,
                Password = userProfileViewModel.Password, // need to hash
                Phone = userProfileViewModel.Phone,
                State = userProfileViewModel.State,
                Zipcode = userProfileViewModel.Zipcode,
                IsAdmin = userProfileViewModel.IsAdmin
            };
            _context.UserProfile.Add(newProfile);
            await _context.SaveChangesAsync();
            userProfileViewModel.UserProfileId = newProfile.UserProfileId;
            return userProfileViewModel;
        }

        private UserProfileViewModel PopulateUserProfileViewModel(UserProfile userProfile)
        {
            return new UserProfileViewModel
            {
                AddressLine1 = userProfile.AddressLine1,
                AddressLine2 = userProfile.AddressLine2,
                City = userProfile.City,
                Email = userProfile.Email,
                FirstName = userProfile.FirstName,
                MiddleName = userProfile.MiddleName,
                LastName = userProfile.LastName,
                Phone = userProfile.Phone,
                State = userProfile.State,
                Zipcode = userProfile.Zipcode,
                IsAdmin = userProfile.IsAdmin ?? false
            };
        }
    }
}
