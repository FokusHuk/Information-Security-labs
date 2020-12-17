using System.Numerics;

namespace InfSec.SRP
{
    public class SRPServer
    {
        private SRPFactors factors;
        private string username;
        private string s;
        private BigInteger v;

        public SRPServer(SRPFactors factors)
        {
            this.factors = factors;
        }

        public void GetDataFromClient(string username, string s, BigInteger v)
        {
            this.username = username;
            this.s = s;
            this.v = v;
        }
    }
}