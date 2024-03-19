using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CoachAssist.Application.Helpers
{
    public class PasswordHashing
    {
        public static string Hash(string password, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(salt);

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password, bytes, KeyDerivationPrf.HMACSHA256, 10000, 32));
        }

        public static (string Hash, string Salt) HashWithSalt(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(128 / 8);
            var saltStr = Convert.ToBase64String(saltBytes);

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, Encoding.UTF8.GetBytes(saltStr), KeyDerivationPrf.HMACSHA256, 10000, 32));

            return (hash, saltStr);
        }
    }
}