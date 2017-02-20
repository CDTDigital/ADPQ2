using System.Collections.Generic;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            UserMessage = new HashSet<UserMessage>();
        }

        public int UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual ICollection<UserMessage> UserMessage { get; set; }
    }
}
