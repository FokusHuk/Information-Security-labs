using System;
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

        private BigInteger A;
        private BigInteger B;
        private BigInteger S;
        private BigInteger K;
        
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
            x = ShaHashing.GenerateSha512Hash(s + password);
            v = BigInteger.ModPow(factors.g, x, factors.N);

            server.RegisterClient(username, s, v);
        }

        public void Authentication()
        {
            var random = new Random();
            BigInteger a = random.Next(int.MaxValue / 2, int.MaxValue);
            A = BigInteger.ModPow(factors.g, a, factors.N);

            var response = server.AuthenticateClient(username, A);
            B = response.B;
            
            if (B == 0)
                throw new AuthenticationFailedException();

            var u = ShaHashing.GenerateSha512Hash(A + B.ToString());
            if (u == 0)
                throw new ConnectionInterruptedException();

            S = BigInteger.ModPow(
                B - factors.k * BigInteger.ModPow(factors.g, x, factors.N),
                (a + BigInteger.Multiply(u, x)),
                factors.N);

            K = ShaHashing.GenerateSha512Hash(S.ToString());
            
            GenerateConfirmation();
        }

        public void GenerateConfirmation()
        {
            var M = ShaHashing.GenerateSha512Hash(
                XOR(
                    ShaHashing.GenerateSha512Hash(factors.N.ToString()).ToByteArray(),
                    ShaHashing.GenerateSha512Hash(factors.g.ToString()).ToByteArray())
                + ShaHashing.GenerateSha512Hash(username)
                + S + A.ToString() + B.ToString() + factors.k);

            var serverR = server.ConfirmClientAccess(M);
            
            var clientR = ShaHashing.GenerateSha512Hash(
                A.ToString() + M.ToString() + K.ToString());
            
            if (clientR != serverR)
                throw new ConfirmationFailedException();

            Console.WriteLine("Success!");
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

        private string XOR(byte[] key, byte[] PAN)
        {
            if (key.Length == PAN.Length)
            {
                byte[] result = new byte[key.Length];
                for (int i = 0; i < key.Length; i++)
                {
                    result[i] = (byte) (key[i] ^ PAN[i]);
                }

                string hex = BitConverter.ToString(result).Replace("-", "");
                return hex;
            }

            throw new ArgumentException("Lengths are different");
        }
    }
}