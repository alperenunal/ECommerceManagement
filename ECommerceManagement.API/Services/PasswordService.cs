using System.Security.Cryptography;
using System.Text;

namespace ECommerceManagement.API.Services
{
    public static class PasswordService
    {
        public static string GetSalt()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GetHash(string password, string salt)
        {
            string hash = string.Empty;
            using (var sha = SHA256.Create())
            {
                var concat = password + salt;
                var concatByte = Encoding.UTF8.GetBytes(concat);
                var computedHash = sha.ComputeHash(concatByte);
                foreach (var b in computedHash)
                {
                    hash += $"{b:X2}";
                }
            }
            return hash.ToLower();
        }
    }
}
