﻿namespace InfSec.SRP
{
    public class SRPManager: EncryptionAlgorithmManager
    {
        public SRPManager(InfSecSettings settings) : base(settings)
        {
            Name = "SRP";
        }

        public override string Name { get; }
        
        public override void Execute()
        {
            var factors = new SRPFactors(100000000, 3);
        }
    }
}