using System.Numerics;

namespace InfSec.Settings
{
    public class RSASettings
    {
        public RSASettings(BigInteger message)
        {
            Message = message;
        }
        
        public BigInteger Message { get; }
    }
}