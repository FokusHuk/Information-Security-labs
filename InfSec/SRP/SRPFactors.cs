using System;
using System.Numerics;

namespace InfSec.SRP
{
    public class SRPFactors
    {
        public BigInteger N { get; private set; }
        public BigInteger g { get; }
        public BigInteger k { get; }
        
        private static Random _random = new Random();

        public SRPFactors(int gg, int kk)
        {
            string modulus = "20E176988FD33DE7AE0D296BF805A49F3F45B92FB59036DCC9F0624B89B2DB67";
            N = ShaHashing.CreateBigInteger(modulus, 16);
            g = ShaHashing.CreateBigInteger("" + 10, 10);
            k = ShaHashing.CreateBigInteger("3", 10);
        }

        private void CalculateN()
        {
            BigInteger q;

            do
            {
                q = 0;
                while(!SimpleNumbersGenerator.MillerRabinTest(q, 5))
                {
                    q = _random.Next(int.MaxValue / 2, int.MaxValue);
                }

                N = 2 * q + 1;
            } while(!SimpleNumbersGenerator.MillerRabinTest(N, 5));

            Console.WriteLine("N calculated");
        }
    }
}