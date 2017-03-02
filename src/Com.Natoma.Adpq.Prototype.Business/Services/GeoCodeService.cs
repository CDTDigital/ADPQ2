using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Options;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using GeoCoordinatePortable;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Com.Natoma.Adpq.Prototype.Business.Services
{
    public class GeoCodeService: IGeoCodeService
    {
        private readonly adpq2adpqContext _adpq2AdpqContext;
        private readonly IOptions<GeoCodeOptions> _geoCodeOptions;

        public GeoCodeService(adpq2adpqContext adpq2AdpqContext, IOptions<GeoCodeOptions> geoCodeOptions)
        {
            _adpq2AdpqContext = adpq2AdpqContext;
            _geoCodeOptions = geoCodeOptions;
        }

        /// <summary>
        /// Uses Google Maps API to Geolocate an address
        /// </summary>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zipcode"></param>
        /// <returns></returns>
        public LatLongSet GetGeoLocation(string address1, string address2, string city, string state, string zipcode)
        {
            if (string.IsNullOrEmpty(address1) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(state))
            {
                return new LatLongSet();
            }
            LatLongSet latLongSet;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_geoCodeOptions.Value.BaseAddress);
                    var apiAddress = _geoCodeOptions.Value.ApiUrl + "address=" + address1 + "+" +
                                     city + "+" + state + "+" +
                                     zipcode + "&key=" + _geoCodeOptions.Value.ApiKey;
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
                catch (Exception)
                {
                    return new LatLongSet();
                }

            }
        }

        /// <summary>
        /// Returns a list of users in a radius
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="radiusMiles"></param>
        /// <returns></returns>
        public List<User> GetUsersInRadius(double latitude, double longitude, int radiusMiles)
        {
            var radiusMeters = radiusMiles * 1609.344;  // 1609.344 meters per mile
            var center = new GeoCoordinate(latitude, longitude);
            var result = _adpq2AdpqContext.User.Select(x => new { coord = new GeoCoordinate((double)x.Latitude, (double)x.Longitude), user = x })
                                  .Where(x => x.coord.GetDistanceTo(center) < radiusMeters && ((x.user.IsEmailNotification || x.user.IsSms) && !x.user.IsAdmin))
                                  .Select(x => x.user).ToList();
            return result;
        }
    }
}
