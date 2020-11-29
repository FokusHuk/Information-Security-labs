using System;

namespace InfSec
{
    public class AlgorithmRSA
    {
        public AlgorithmRSA(long p, long q)
        {
            P = p;
            Q = q;
        }

        public void GenerateKeys()
        {
            var n = P * Q;
            var eulerFunc = (P - 1) * (Q - 1);
            var e = calculate_E(eulerFunc);
            var d = Calculate_D(e, eulerFunc);

            PublicKey = new KeyForRSA(e, n);
            PrivateKey = new KeyForRSA(d, n);
        }

        public long Encryption(long messageToEncrypt)
        {
            return (long)(Math.Pow(messageToEncrypt, PublicKey.EXP) % PublicKey.VALUE);
        }

        public long Decryption(long encryptedMessage)
        {
            return (long)(Math.Pow(encryptedMessage, PrivateKey.EXP) % PrivateKey.VALUE);
        }

        private long calculate_E(long eulerFunc)
        {
            long e = 1;
 
            for (long i = 2; i <= eulerFunc; i++)
                if (isMutuallySimple(i, eulerFunc))
                {
                    e = i;
                    break;
                }
 
            return e;
        }

        private bool isMutuallySimple(long num1, long num2)
        {
            var min = Math.Min(num1, num2);
            
            for (long i = 2; i <= min; i++)
            {
                if (num1 % i == 0 && num2 % i == 0)
                    return false;
            }

            return true;
        }
        
        private long Calculate_D(long e, long eulerFunc)
        {
            long d = 3;

            while (true)
            {
                if ((e * d) % eulerFunc == 1)
                    break;
                d++;
            }

            return d;
        }

        public long P { get; }
        public long Q { get; }
        public KeyForRSA PublicKey { get; private set; }
        public KeyForRSA PrivateKey { get; private set; }
    }

    public class KeyForRSA
    {
        public KeyForRSA(long exp, long value)
        {
            EXP = exp;
            VALUE = value;
        }
        
        public long EXP { get; }
        public long VALUE { get; }
    }
}