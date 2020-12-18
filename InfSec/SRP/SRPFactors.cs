using System;
using System.Numerics;

namespace InfSec.SRP
{
    public class SRPFactors
    {
        public BigInteger N { get; private set; }
        public BigInteger g { get; }
        public BigInteger k { get; }

        public SRPFactors(BigInteger g, BigInteger k)
        {
            this.g = g;
            this.k = k;
            CalculateN();
        }

        private void CalculateN()
        {
            var random = new Random();
            BigInteger q;

            do
            {
                do
                {
                    q = random.Next(int.MaxValue / 2, int.MaxValue);
                } while (!SimpleNumbersGenerator.MillerRabinTest(q, 5));

                N = 2 * q + 1;
            } while (!SimpleNumbersGenerator.MillerRabinTest(N, 5));
        }
    }
}