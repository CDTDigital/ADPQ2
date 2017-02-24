using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Models.Auth;
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

        public UserProfileService(adpq2adpqContext context)
        {
            _context = context;
        }

        public async Task<UserProfileViewModel> Get(int id)
        {
            var result = await _context.User.Where(x => x.UserId == id).FirstOrDefaultAsync();
            return result == null ? null : PopulateUserProfileViewModel(result) ;
        }

        public async Task<UserProfileViewModel> Create(UserProfileViewModel userProfileViewModel)
        {
            var latLongSet = GetGeoLocation(userProfileViewModel.AddressLine1, null, userProfileViewModel.City, userProfileViewModel.State, userProfileViewModel.Zipcode);

            var passwordHashSet = PasswordUtils.GetSaltAndHashValue(userProfileViewModel.Password);

            try
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return userProfileViewModel;
        }

        public LatLongSet GetGeoLocation(string address1, string address2, string city, string state, string zipcode)
        {
            if (string.IsNullOrEmpty(address1) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(state))
            {
                return new LatLongSet();
            }
            LatLongSet latLongSet = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://maps.googleapis.com");
                    var apiAddress = "/maps/api/geocode/json?" + "address=" + address1 + "+" +
                                     city + "+" + state + "+" +
                                     zipcode + "&key=AIzaSyDMLoJ5K4BFV8Jqwt22R3UIrJGH_zMAe7A";
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync(apiAddress).Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;

                    dynamic data = JsonConvert.DeserializeObject<dynamic>(stringData);
                    var results = data.results;
                    var geometry = results[0].geometry;
                    var location = geometry.location;
                    latLongSet = new LatLongSet
                    {
                        Latitude = location.lat,
                        Longitude = location.lng
                    };
                    return latLongSet;
                }
                catch (Exception e)
                {
                    return new LatLongSet();
                }
                
            }    
        }

        private UserProfileViewModel PopulateUserProfileViewModel(User userProfile)
        {
            return new UserProfileViewModel
            {
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
