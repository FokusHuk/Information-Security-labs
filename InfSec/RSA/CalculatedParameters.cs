using System.Numerics;

namespace InfSec
{
    public class CalculatedParameters
    {
        public CalculatedParameters(
            BigInteger p, 
            BigInteger q, 
            BigInteger n, 
            BigInteger eulerFunc, 
            BigInteger e, 
            BigInteger d)
        {
            P = p;
            Q = q;
            N = n;
            EulerFunc = eulerFunc;
            E = e;
            D = d;
        }

        public override string ToString()
        {
            return $"P = {P}\nQ = {Q}\nN = {N}\nEulerFunc = {EulerFunc}\nE = {E}\nD = {D}";
        }

        public BigInteger P {get; }
        public BigInteger Q {get; }
        public BigInteger N {get; }
        public BigInteger EulerFunc { get;}
        public BigInteger E { get; }
        public BigInteger D { get; }
    }
}