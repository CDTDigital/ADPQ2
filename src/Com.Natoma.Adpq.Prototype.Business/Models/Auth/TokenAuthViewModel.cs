namespace Com.Natoma.Adpq.Prototype.Business.Models.Auth
{
    public class TokenAuthViewModel
    {
        public int UserProfileId { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}
