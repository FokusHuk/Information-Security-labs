namespace InfSec
{
    public class DHSettings
    {
        public DHSettings(double g, double p)
        {
            G = g;
            P = p;
        }
        
        public double G { get; }
        public double P { get; }
    }
}