using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Com.Natoma.Adpq.Prototype.Business.Models.Auth
{
    public class TokenAuthOption
    {
        public static string Audience { get; } = "https://example.com";
        public static string Issuer { get; } = "noreply@example.com";
        public static SecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("qhMIqDeTb5v31ZM3nyKo"));
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        public static string TokenType { get; } = "Bearer";
    }
}
