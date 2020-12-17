using System;
using System.Linq;
using System.Numerics;

namespace InfSec.SRP
{
    public class SRPClient
    {
        public string s { get; private set; }
        public BigInteger x { get; private set; }
        private SRPFactors factors { get; }
        public string username { get; }
        public string password { get; }
        public BigInteger v { get; private set; }
        
        public SRPClient(SRPFactors factors, string username, string password)
        {
            this.factors = factors;
            this.username = username;
            this.password = password;
        }

        public void Registration()
        {
            generateS();
            x = factors.ShaHashing.GenerateSha512Hash(mixPassword());
            v = BigInteger.ModPow(factors.g, x, factors.N);
        }

        private void generateS()
        {
            var random = new Random();
            var sLength = random.Next(20, 50);
            s = "";

            for (int i = 0; i < sLength; i++)
            {
                var isUppercase = random.Next() % 2 == 0;
                if (isUppercase)
                {
                    s += (char)random.Next('A', 'Z');
                }
                else
                {
                    s += (char)random.Next('a', 'z');
                }
            }
        }

        private string mixPassword()
        {
            var random = new Random();
            return s.Insert(random.Next(s.Length), password);
        }
    }
}