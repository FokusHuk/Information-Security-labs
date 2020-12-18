using System.Numerics;

namespace InfSec.Settings
{
    public class DHSettings
    {
        public DHSettings(BigInteger g, BigInteger p)
        {
            G = g;
            P = p;
        }
        
        public BigInteger G { get; }
        public BigInteger P { get; }
    }
}