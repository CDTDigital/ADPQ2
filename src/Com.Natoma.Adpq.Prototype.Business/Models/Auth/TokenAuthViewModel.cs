using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Natoma.Adpq.Prototype.Business.Models.Auth
{
    public class TokenAuthViewModel
    {
        public int UserProfileId { get; set; }
        public string Token { get; set; }
    }
}
