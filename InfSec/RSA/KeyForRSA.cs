using System.Numerics;

namespace InfSec
{
    public class KeyForRSA
    {
        public KeyForRSA(BigInteger exp, BigInteger value)
        {
            EXP = exp;
            VALUE = value;
        }
        
        public BigInteger EXP { get; }
        public BigInteger VALUE { get; }
    }
}