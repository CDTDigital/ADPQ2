using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Com.Natoma.Adpq.Prototype.Business.Utils
{
    public class PasswordUtils
    {
        public static SaltHashSet GetSaltAndHashValue(string password, byte[] existingSalt = null)
        {
            byte[] salt;
            if (existingSalt != null)
            {
                salt = existingSalt;
            }
            else
            {
                // generate a 128-bit salt using a secure PRNG
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                var byteBase64String = Convert.ToBase64String(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));

            return new SaltHashSet
            {
                Salt = salt,
                SaltBase64String = Convert.ToBase64String(salt),
                Hashed = hashed
            };
        }
    }

    public class SaltHashSet
    {
        public byte[] Salt { get; set; }
        public string SaltBase64String { get; set; }
        public string Hashed { get; set; }
    }

}
