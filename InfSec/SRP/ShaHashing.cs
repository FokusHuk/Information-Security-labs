using System.Security.Cryptography;
using System.Text;

namespace InfSec.SRP
{
    public class ShaHashing
    {
        public string HashPassword(string password)
        {
            return GenerateSha512String(password);
        }

        public bool Verify(string password, string hashedPassword)
        {
            var hashed = GenerateSha512String(password);
            return hashed == hashedPassword;
        }

        public string GenerateSha512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}