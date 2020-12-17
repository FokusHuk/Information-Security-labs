using System;
using System.Numerics;

namespace InfSec.SRP
{
    public class SRPFactors
    {
        public ShaHashing ShaHashing { get; }
        public BigInteger N { get; private set; }
        public int g { get; }
        public int k { get; }

        public SRPFactors(int g, int k)
        {
            this.g = g;
            this.k = k;
            ShaHashing = new ShaHashing();
            CalculateN();
        }

        private void CalculateN()
        {
            var random = new Random();
            BigInteger q = 0;

            do
            {
                while(!SimpleNumbersGenerator.MillerRabinTest(q, 5))
                {
                    q = random.Next(1000000000);
                }

                N = 2 * q + 1;
            } while(!SimpleNumbersGenerator.MillerRabinTest(N, 5));
        }
    }
}