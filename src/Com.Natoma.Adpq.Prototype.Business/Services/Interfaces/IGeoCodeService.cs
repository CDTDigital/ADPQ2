using System.Collections.Generic;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;

namespace Com.Natoma.Adpq.Prototype.Business.Services.Interfaces
{
    public interface IGeoCodeService
    {
        LatLongSet GetGeoLocation(string address1, string address2, string city, string state, string zipcode);
        List<User> GetUsersInRadius(double latitude, double longitude, int radiusMiles);
    }
}
