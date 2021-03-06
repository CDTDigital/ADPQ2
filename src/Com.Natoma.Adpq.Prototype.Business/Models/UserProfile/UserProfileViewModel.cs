﻿namespace Com.Natoma.Adpq.Prototype.Business.Models.UserProfile
{
    public class UserProfileViewModel
    {
        public int UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEmailNotifications { get; set; }
        public bool IsSms { get; set; }
        public string Token { get; set; }
    }
}
