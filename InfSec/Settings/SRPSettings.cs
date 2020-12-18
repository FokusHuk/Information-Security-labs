using System.Numerics;

namespace InfSec
{
    public class SRPSettings
    {
        public SRPSettings(BigInteger g, BigInteger k)
        {
            this.g = g;
            this.k = k;
        }

        public BigInteger g { get; }
        public BigInteger k { get; }
    }
}