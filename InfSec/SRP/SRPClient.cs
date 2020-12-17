using System;
using System.Linq;
using System.Numerics;

namespace InfSec.SRP
{
    public class SRPClient
    {
        private SRPFactors factors;
        private SRPServer server;

        private string username;
        private string password;
        
        private string s;
        private BigInteger x;
        private BigInteger v;
        
        public SRPClient(SRPFactors factors, string username, string password)
        {
            this.factors = factors;
            this.username = username;
            this.password = password;
        }

        public void Connect(SRPServer server)
        {
            this.server = server;
        }

        public void Registration()
        {
            generateS();
            x = factors.ShaHashing.GenerateSha512Hash(mixPassword());
            v = BigInteger.ModPow(factors.g, x, factors.N);

            server.RegisterClient(username, s, v);
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