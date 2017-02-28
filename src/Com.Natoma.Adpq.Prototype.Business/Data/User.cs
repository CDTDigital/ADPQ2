using System;
using System.Collections.Generic;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class User
    {
        public User()
        {
            UserNotification = new HashSet<UserNotification>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsEmailNotification { get; set; }
        public bool IsSms { get; set; }
        public string Phone { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<UserNotification> UserNotification { get; set; }
    }
}
