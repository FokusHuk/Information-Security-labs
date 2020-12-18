using System;
using System.Numerics;
using System.Security.Cryptography;
using InfSec.Settings;

namespace InfSec
{
    public static class SimpleNumbersGenerator
    {
        public static BigInteger GetBigSimpleNumber()
        {
            var random = new Random();
            BigInteger randomNumber = 0;

            while (true)
            {
                randomNumber = BigInteger.Parse(random.Next(1, int.MaxValue / 100000).ToString());

                if (MillerRabinTest(randomNumber, _settings.MillerRabinTestIterationsCount))
                    return randomNumber;
            }
        }
        
        public static bool MillerRabinTest(BigInteger n, int k)
        {
            if (n == 2 || n == 3)
                return true;
            
            if (n < 2 || n % 2 == 0)
                return false;
            
            BigInteger t = n - 1;
            int s = 0;

            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }
            
            for (int i = 0; i < k; i++)
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                var _a = new byte[n.ToByteArray().LongLength];
                BigInteger a;

                do
                {
                    rng.GetBytes(_a);
                    a = new BigInteger(_a);
                }
                while (a < 2 || a >= n - 2);
                
                BigInteger x = BigInteger.ModPow(a, t, n);
                
                if (x == 1 || x == n - 1)
                    continue;
                
                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, n);
                    
                    if (x == 1)
                        return false;
                    
                    if (x == n - 1)
                        break;
                }

                if (x != n - 1)
                    return false;
            }
            
            return true;
        }

        private static InfSecSettings _settings = InfSecSettings.FromJsonFile("./appsettings.json");
    }
}