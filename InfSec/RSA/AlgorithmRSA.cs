using System.Numerics;

namespace InfSec.RSA
{
    public class AlgorithmRSA
    {
        public AlgorithmRSA()
        {
            
        }

        public void GenerateKeys()
        {
            var p = SimpleNumbersGenerator.GetBigSimpleNumber();
            var q = SimpleNumbersGenerator.GetBigSimpleNumber();
            var n = p * q;
            var eulerFunc = (p - 1) * (q - 1);
            var e = calculate_E(eulerFunc);
            var d = Calculate_D(e, eulerFunc);

            PK = new KeyForRSA(e, n);
            SK = new KeyForRSA(d, n);
            CalculatedParameters = new CalculatedParameters(p, q, n, eulerFunc, e, d);
        }

        public BigInteger Encryption(BigInteger messageToEncrypt)
        {
            return BigInteger.ModPow(messageToEncrypt, PK.EXP, PK.VALUE);
        }

        public BigInteger Decryption(BigInteger encryptedMessage)
        {
            return BigInteger.ModPow(encryptedMessage, SK.EXP, SK.VALUE);
        }

        private long calculate_E(BigInteger eulerFunc)
        {
            for (long e = 3; e <= eulerFunc; e += 2)
                if (isMutuallySimple(e, eulerFunc))
                {
                    return e;
                }
 
            return 3;
        }

        private bool isMutuallySimple(BigInteger num1, BigInteger num2)
        {
            BigInteger min = num1;
            if (num2 < num1)
                min = num2;

            for (BigInteger i = 3; i <= min; i++)
            {
                if (num1 % i == 0 && num2 % i == 0)
                    return false;
            }

            return true;
        }
        
        private BigInteger Calculate_D(BigInteger e, BigInteger eulerFunc)
        {
            BigInteger d = 3;

            while (true)
            {
                if (BigInteger.ModPow(e * d, 1, eulerFunc) == 1)
                    break;
                d++;
            }

            return d;
        }
        
        public KeyForRSA PK { get; private set; }
        public KeyForRSA SK { get; private set; }
        
        public CalculatedParameters CalculatedParameters { get; private set; }
    }
}