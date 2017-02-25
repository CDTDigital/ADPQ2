using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Newtonsoft.Json;

namespace Com.Natoma.Adpq.Prototype.Business.Services
{
    public class GeoCodeService: IGeoCodeService
    {
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
                catch (Exception)
                {
                    return new LatLongSet();
                }

            }
        }
    }
}
