using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Models.Auth;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Com.Natoma.Adpq.Prototype.Business.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace Com.Natoma.Adpq.Prototype.Business.Services
{
    public class UserProfileService: IUserProfileService
    {
        private readonly adpq2adpqContext _context;
        private readonly IGeoCodeService _geoCodeService;

        public UserProfileService(adpq2adpqContext context, IGeoCodeService geoCodeService)
        {
            _context = context;
            _geoCodeService = geoCodeService;
        }

        public async Task<RequestResult> Get(int id)
        {
            var user = await _context.User.Where(x => x.UserId == id).FirstOrDefaultAsync();
            var result = user == null ? null : PopulateUserProfileViewModel(user) ;
            return new RequestResult
            {
                State = RequestStateEnum.Success,
                Data = result
            };
        }

        public async Task<RequestResult> Get(string email, string password)
        {
            // get user by email address
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                var passwordHashCompare = PasswordUtils.GetSaltAndHashValue(password,
                    Convert.FromBase64String(user.PasswordSalt));

                if (passwordHashCompare.Hashed == user.Password)
                {
                    // winner
                    return new RequestResult
                    {
                        State = RequestStateEnum.Success,
                        Data = PopulateUserProfileViewModel(user)
                    };
                }
            }

            return new RequestResult
            {
                State = RequestStateEnum.NotAuth
            };
        }

        public async Task<RequestResult> Create(UserProfileViewModel userProfileViewModel)
        {
            // check for unique email
            if (_context.User.Any(x => x.Email == userProfileViewModel.Email))
            {
                return new RequestResult
                {
                    State = RequestStateEnum.Failed,
                    Msg = "Email already exists."
                };
            }

            // get latlong set
            var latLongSet = _geoCodeService.GetGeoLocation(userProfileViewModel.AddressLine1, null, userProfileViewModel.City, userProfileViewModel.State, userProfileViewModel.Zipcode);

            // get the password hash
            var passwordHashSet = PasswordUtils.GetSaltAndHashValue(userProfileViewModel.Password);
   
            var newProfile = new User
            {
                Address1 = userProfileViewModel.AddressLine1,
                Address2 = userProfileViewModel.AddressLine2,
                City = userProfileViewModel.City,
                Email = userProfileViewModel.Email,
                FirstName = userProfileViewModel.FirstName,
                LastName = userProfileViewModel.LastName,
                Password = passwordHashSet.Hashed, // one way hash of password
                PasswordSalt = passwordHashSet.SaltBase64String, // base 64 string of salt used for hash
                State = userProfileViewModel.State,
                Zipcode = userProfileViewModel.Zipcode,
                IsAdmin = userProfileViewModel.IsAdmin,
                Latitude = latLongSet.Latitude,
                Longitude = latLongSet.Longitude,
                IsEmailNotification = userProfileViewModel.IsEmailNotifications,
                IsSms = userProfileViewModel.IsSms,
                Phone = userProfileViewModel.Phone
            };
            _context.User.Add(newProfile);
            await _context.SaveChangesAsync();
            userProfileViewModel.UserProfileId = newProfile.UserId;
            // generate a JWT token to return with the user
            var requestAt = DateTime.Now;
            var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
            userProfileViewModel.Token = TokenAuthUtils.GenerateToken(userProfileViewModel, expiresIn);
            userProfileViewModel.Password = "";

            return new RequestResult
            {
                State = RequestStateEnum.Success,
                Data = userProfileViewModel
            }; 
        }

        public async Task<RequestResult> Update(UserProfileViewModel userProfileViewModel)
        {
            var updatingUserProfile = _context.User.FirstOrDefault(x => x.UserId == userProfileViewModel.UserProfileId);

            if (updatingUserProfile == null)
            {
                return new RequestResult
                {
                    State = RequestStateEnum.Failed,
                    Msg = "User not found"
                };
            }

            updatingUserProfile.UserId = userProfileViewModel.UserProfileId;
            updatingUserProfile.Email = userProfileViewModel.Email;
            updatingUserProfile.Address1 = userProfileViewModel.AddressLine1;
            updatingUserProfile.Address2 = userProfileViewModel.AddressLine2;
            updatingUserProfile.City = userProfileViewModel.City;
            updatingUserProfile.State = userProfileViewModel.State;
            updatingUserProfile.Zipcode = userProfileViewModel.Zipcode;
            updatingUserProfile.Phone = userProfileViewModel.Phone;
            updatingUserProfile.FirstName = userProfileViewModel.FirstName;
            updatingUserProfile.LastName = userProfileViewModel.LastName;
            updatingUserProfile.IsAdmin = userProfileViewModel.IsAdmin;
            updatingUserProfile.IsEmailNotification = userProfileViewModel.IsEmailNotifications;
            updatingUserProfile.IsSms = userProfileViewModel.IsSms;

            // get latlong set
            var latLongSet = _geoCodeService.GetGeoLocation(userProfileViewModel.AddressLine1, null, userProfileViewModel.City, userProfileViewModel.State, userProfileViewModel.Zipcode);

            updatingUserProfile.Latitude = latLongSet.Latitude;
            updatingUserProfile.Longitude = latLongSet.Longitude;

            await _context.SaveChangesAsync();

            return new RequestResult
            {
                State = RequestStateEnum.Success,
                Data = userProfileViewModel,
                Msg = "User updated."
            };
        }
        
        private UserProfileViewModel PopulateUserProfileViewModel(User userProfile)
        {
            return new UserProfileViewModel
            {
                UserProfileId = userProfile.UserId,
                AddressLine1 = userProfile.Address1,
                AddressLine2 = userProfile.Address2,
                City = userProfile.City,
                Email = userProfile.Email,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                State = userProfile.State,
                Zipcode = userProfile.Zipcode,
                IsAdmin = userProfile.IsAdmin ?? false
            };
        }
    }
}
