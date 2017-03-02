using System;
using Com.Natoma.Adpq.Prototype.Business.Utils;
using Microsoft.IdentityModel.Tokens;

namespace Com.Natoma.Adpq.Prototype.Business.Models.Auth
{
    public class TokenAuthOption
    {
        public static string Audience { get; } = "https://exmample.com";
        public static string Issuer { get; } = "noreply@example.com";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        public static string TokenType { get; } = "Bearer";
    }
}
