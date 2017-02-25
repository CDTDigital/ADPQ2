﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;

namespace Com.Natoma.Adpq.Prototype.Business.Services.Interfaces
{
    public interface IGeoCodeService
    {
        LatLongSet GetGeoLocation(string address1, string address2, string city, string state, string zipcode);
    }
}
