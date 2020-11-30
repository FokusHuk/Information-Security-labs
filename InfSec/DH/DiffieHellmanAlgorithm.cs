using System;
using System.Numerics;

namespace InfSec
{
    public class DiffieHellmanAlgorithm
    {
        public DiffieHellmanAlgorithm(BigInteger paramG, BigInteger paramP)
        {
            ParamG = paramG;
            ParamP = paramP;
        }

        public void GenerateNumber()
        {
            var random = new Random();
            LocalGeneratedParameter = BigInteger.Parse(random.Next(int.MaxValue / 2, int.MaxValue).ToString());
        }
        
        public BigInteger CalculateParameterToSend()
        {
            return BigInteger.ModPow(ParamG, LocalGeneratedParameter, ParamP);
        }

        public void CalculateKey(BigInteger receivedParameter)
        {
            Key =  BigInteger.ModPow(receivedParameter, LocalGeneratedParameter, ParamP);
        }

        public BigInteger ParamG { get; }
        public BigInteger ParamP { get; }
        public BigInteger LocalGeneratedParameter { get; private set; }
        public BigInteger Key { get; private set; }
    }
}