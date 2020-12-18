using System;
using System.Numerics;

namespace InfSec.SRP
{
    public class SRPServer
    {
        private SRPFactors factors;
        private SRPClient client;
        
        private string username;
        private string s;
        
        private BigInteger v;
        
        private BigInteger A;
        private BigInteger B;
        private BigInteger S;
        private BigInteger K;

        public SRPServer(SRPFactors factors)
        {
            this.factors = factors;
        }

        public void Connect(SRPClient client)
        {
            this.client = client;
        }

        public void RegisterClient(string username, string s, BigInteger v)
        {
            this.username = username;
            this.s = s;
            this.v = v;
        }

        public ServerAuthResponse AuthenticateClient(string username, BigInteger A)
        {
            if (A == 0)
                throw new AuthenticationFailedException();
            
            this.A = A;
            var random = new Random();
            BigInteger b = random.Next(int.MaxValue / 2, int.MaxValue);
            B = factors.k * v + BigInteger.ModPow(factors.g, b, factors.N);

            var u = factors.ShaHashing.GenerateSha512Hash(A + B.ToString());
            if (u == 0)
                throw new ConnectionInterruptedException();

            S = BigInteger.ModPow(
                A * BigInteger.ModPow(v, u, factors.N),
                b,
                factors.N);

            K = factors.ShaHashing.GenerateSha512Hash(S.ToString());
            
            return new ServerAuthResponse(s, B);
        }

        public BigInteger ConfirmClientAccess(BigInteger clientM)
        {
            var serverM = factors.ShaHashing.GenerateSha512Hash(
                XOR(
                    factors.ShaHashing.GenerateSha512Hash(factors.N.ToString()).ToByteArray(),
                    factors.ShaHashing.GenerateSha512Hash(factors.g.ToString()).ToByteArray())
                + factors.ShaHashing.GenerateSha512Hash(username)
                + S + A.ToString() + B.ToString() + factors.k);

            if (serverM != clientM)
                throw new ConfirmationFailedException();

            var R = factors.ShaHashing.GenerateSha512Hash(
                A.ToString() + serverM.ToString() + K.ToString());

            return R;
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
            else
            {
                throw new ArgumentException("Lengths are different");
            }
        }
    }
}