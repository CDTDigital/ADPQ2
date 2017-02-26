namespace Com.Natoma.Adpq.Prototype.Business.Models.Notification
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }
        public NotificationTypeEnum NotificationType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public string SmsMessage { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int RadiusMiles { get; set; }
        public int NumberOfRecipients { get; set; }
        public int CreatedBy { get; set; }
    }
}
