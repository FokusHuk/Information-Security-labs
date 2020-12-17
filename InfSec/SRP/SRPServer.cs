using System.Numerics;

namespace InfSec.SRP
{
    public class SRPServer
    {
        private SRPFactors factors;
        private SRPClient client;
        
        private string username;
        private string s;
        
        private BigInteger v;

        public SRPServer(SRPFactors factors)
        {
            this.factors = factors;
        }

        public void Connect(SRPClient client)
        {
            this.client = client;
        }

        public void RegisterClient(string username, string s, BigInteger v)
        {
            this.username = username;
            this.s = s;
            this.v = v;
        }
    }
}