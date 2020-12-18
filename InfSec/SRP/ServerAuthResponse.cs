using System.Numerics;

namespace InfSec.SRP
{
    public class ServerAuthResponse
    {
        public string s { get; }
        public BigInteger B { get; }

        public ServerAuthResponse(string s, BigInteger b)
        {
            this.s = s;
            B = b;
        }
    }
}