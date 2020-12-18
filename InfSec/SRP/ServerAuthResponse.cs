using System.Numerics;

namespace InfSec.SRP
{
    public class ServerAuthResponse
    {
        public BigInteger s { get; }
        public BigInteger B { get; }

        public ServerAuthResponse(BigInteger s, BigInteger b)
        {
            this.s = s;
            B = b;
        }
    }
}