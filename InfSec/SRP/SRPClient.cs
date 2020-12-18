﻿using System;
using System.Collections;
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
            x = factors.ShaHashing.GenerateSha512Hash(s + password);
            v = BigInteger.ModPow(factors.g, x, factors.N);

            server.RegisterClient(username, s, v);
        }

        public void Authentication()
        {
            var random = new Random();
            BigInteger a = random.Next(1000000000);
            A = BigInteger.ModPow(factors.g, a, factors.N);

            var response = server.AuthenticateClient(username, A);
            B = response.B;
            
            if (B == 0)
                throw new AuthenticationFailedException();

            var u = factors.ShaHashing.GenerateSha512Hash(A.ToString() + B.ToString());
            if (u == 0)
                throw new ConnectionInterruptedException();

            S = BigInteger.ModPow(
                BigInteger.Pow(
                    B - factors.k * BigInteger.ModPow(factors.g, x, factors.N),
                    (int)(a + BigInteger.Multiply(u,  x))),
                1,
                factors.N);

            K = factors.ShaHashing.GenerateSha512Hash(S.ToString());
            
            GenerateConfirmation();
        }

        public void GenerateConfirmation()
        {
            var M = factors.ShaHashing.GenerateSha512Hash(
                XOR(
                    factors.ShaHashing.GenerateSha512Hash(factors.N.ToString()).ToByteArray(),
                    factors.ShaHashing.GenerateSha512Hash(factors.g.ToString()).ToByteArray())
                + factors.ShaHashing.GenerateSha512Hash(username)
                + S + A.ToString() + B.ToString() + factors.k);

            var serverR = server.ConfirmClientAccess(M);
            
            var clientR = factors.ShaHashing.GenerateSha512Hash(
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

            Console.WriteLine("S calculated.");
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