using System;
using System.Collections;
using System.Numerics;
using System.Text;

namespace InfSec.SRP
{
    public class SRPClient
    {
        private SRPFactors factors;
        private SRPServer server;

        private string username;
        private string password;
        
        private BigInteger s;
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
            s = ShaHashing.CreateBigInteger(512, new Random());

            byte[] identityHash = ShaHashing.GenerateSha512Hash(Encoding.Unicode.GetBytes((username + ":" + password)));
            
            x = ShaHashing.CreateSaltedIdentityHash(s, identityHash);
            
            v = BigInteger.ModPow(factors.g, x, factors.N);

            server.RegisterClient(username, s, v);
        }

        public void Authentication()
        {
            var random = new Random();
            BigInteger a = ShaHashing.GeneratePseudoPrime(128, 100, new Random());
            A = BigInteger.ModPow(factors.g, a, factors.N);

            var response = server.AuthenticateClient(username, A);
            B = response.B;
            
            if (B == 0)
                throw new AuthenticationFailedException();

            var u = ShaHashing.CreateBigInteger(256, new Random());
            if (u == 0)
                throw new ConnectionInterruptedException();

            BigInteger param = BigInteger.Add(
                BigInteger.Multiply(u, v),
                a);
            S = BigInteger.ModPow(
                BigInteger.Subtract(
                B,
                BigInteger.Multiply(
                    BigInteger.ModPow(factors.g, x, factors.N),
                    factors.k)),
                param,
                factors.N);

            K = new BigInteger(ShaHashing.GenerateSha512Hash(S.ToByteArray()));


            var tt = 0;
            //GenerateConfirmation();
        }

        //public void GenerateConfirmation()
        //{
        //    var M = factors.ShaHashing.GenerateSha512Hash(
        //        XOR(
        //            factors.ShaHashing.GenerateSha512Hash(factors.N.ToString()).ToByteArray(),
        //            factors.ShaHashing.GenerateSha512Hash(factors.g.ToString()).ToByteArray())
        //        + factors.ShaHashing.GenerateSha512Hash(username)
        //        + S + A.ToString() + B.ToString() + factors.k);
//
        //    var serverR = server.ConfirmClientAccess(M);
        //    
        //    var clientR = factors.ShaHashing.GenerateSha512Hash(
        //        A.ToString() + M.ToString() + K.ToString());
        //    
        //    if (clientR != serverR)
        //        throw new ConfirmationFailedException();
//
        //    Console.WriteLine("Success!");
        //}

        private string GetSalt()
        {
            var random = new Random();
            var sLength = random.Next(20, 50);
            var salt = "";

            for (int i = 0; i < sLength; i++)
            {
                var isUppercase = random.Next() % 2 == 0;
                if (isUppercase)
                {
                    salt += (char)random.Next('A', 'Z');
                }
                else
                {
                    salt += (char)random.Next('a', 'z');
                }
            }

            Console.WriteLine("S calculated.");

            return salt;
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