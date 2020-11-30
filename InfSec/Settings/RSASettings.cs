namespace InfSec
{
    public class RSASettings
    {
        public RSASettings(int p, int q, int message)
        {
            P = p;
            Q = q;
            Message = message;
        }
        
        public int P { get; }
        public int Q { get; }
        public int Message { get; }
    }
}