using System;

namespace InfSec
{
    public class DiffieHellmanAlgorithm
    {
        public DiffieHellmanAlgorithm(double paramG, double paramP)
        {
            ParamG = paramG;
            ParamP = paramP;
        }

        public void GenerateNumber()
        {
            var random = new Random();
            LocalGeneratedParameter = random.Next(1, 10);
        }
        
        public double CalculateParameterToSend()
        {
            return (Math.Pow(ParamG, LocalGeneratedParameter) % ParamP);
        }

        public void CalculateKey(double receivedParameter)
        {
            Key = (Math.Pow(receivedParameter, LocalGeneratedParameter) % ParamP);
        }

        public double ParamG { get; }
        public double ParamP { get; }
        public double LocalGeneratedParameter { get; private set; }
        public double Key { get; private set; }
    }
}