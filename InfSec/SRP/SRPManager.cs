namespace InfSec.SRP
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
            var client = new SRPClient(factors, "user", "password");
            var server = new SRPServer(factors);
            
            client.Connect(server);
            server.Connect(client);
            
            client.Registration();
            client.Authentication();
        }
    }
}