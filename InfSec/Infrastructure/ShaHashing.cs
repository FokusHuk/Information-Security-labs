using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace InfSec.Infrastructure
{
    public static class ShaHashing
    {
        public static BigInteger GenerateSha512Hash(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return BigInteger.Abs(new BigInteger(hash));
        }
    }
}