using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            var latLongSet = GetGeoLocation(userProfileViewModel.AddressLine1, null, userProfileViewModel.City, userProfileViewModel.State, userProfileViewModel.Zipcode);
            var newProfile = new UserProfile
            {
                AddressLine1 = userProfileViewModel.AddressLine1,
                AddressLine2 = userProfileViewModel.AddressLine2,
                City = userProfileViewModel.City,
                Email = userProfileViewModel.Email,
                FirstName = userProfileViewModel.FirstName,
                LastName = userProfileViewModel.LastName,
                Password = userProfileViewModel.Password, // need to hash
                Phone = userProfileViewModel.Phone,
                State = userProfileViewModel.State,
                Zipcode = userProfileViewModel.Zipcode,
                IsAdmin = userProfileViewModel.IsAdmin,
                Latitude = latLongSet.Latitude,
                Longitude = latLongSet.Longitude
            };
            _context.UserProfile.Add(newProfile);
            await _context.SaveChangesAsync();
            userProfileViewModel.UserProfileId = newProfile.UserProfileId;
            return userProfileViewModel;
        }

        public LatLongSet GetGeoLocation(string address1, string address2, string city, string state, string zipcode)
        {
            LatLongSet latLongSet = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://maps.googleapis.com");
                var apiAddress = "/maps/api/geocode/json?" + "address=" + address1 + "+" +
                                 city + "+" +  state + "+" +
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
            }
            return latLongSet;
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
                LastName = userProfile.LastName,
                Phone = userProfile.Phone,
                State = userProfile.State,
                Zipcode = userProfile.Zipcode,
                IsAdmin = userProfile.IsAdmin ?? false
            };
        }
    }
}
