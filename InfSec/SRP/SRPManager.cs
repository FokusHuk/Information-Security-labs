using System;
using System.Numerics;

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
            var factors = new SRPFactors(_settings.SRP.g, _settings.SRP.k);

            Console.WriteLine($"SRP Factors:\ng: {factors.g}\nk: {factors.k}\nN: {factors.N}\n");
            
            var client = new SRPClient(factors, "user", "password");
            var server = new SRPServer(factors);
            
            client.Connect(server);
            client.Registration();
            client.Authentication();
            
            Console.WriteLine("Success!");
        }

        public static void DisplayRegistration(string s, BigInteger x, BigInteger v)
        {
            Console.WriteLine("================ Registration ================");
            Console.WriteLine("$[Client]");
            Console.WriteLine($"Salt: {s}");
            Console.WriteLine($"Secret key (x): {x}");
            Console.WriteLine($"Verifier (v): {v}");
            Console.WriteLine();
        }

        public static void DisplayAuthenticationBeforeSending(BigInteger a, BigInteger A)
        {
            Console.WriteLine("================ Authentication ================");
            Console.WriteLine("$[Client]");
            Console.WriteLine($"Client private key: {a}");
            Console.WriteLine($"Client public key: {A}");
            Console.WriteLine("\nSending public key to server...");
            Console.WriteLine();
        }
        
        public static void DisplayAuthenticationOnServerSide(BigInteger b, BigInteger B, BigInteger u, BigInteger S, BigInteger K)
        {
            Console.WriteLine("$[Server]");
            Console.WriteLine($"Server private key: {b}");
            Console.WriteLine($"Server public key: {B}");
            Console.WriteLine("\nSending public key to client...\n");
            Console.WriteLine("$[Server]");
            Console.WriteLine($"Scrambler (u): {u}");
            Console.WriteLine($"Session key (S): {S}");
            Console.WriteLine($"Encryption key (K): {K}");
            Console.WriteLine();
        }

        public static void DisplayAuthenticationAfterSending(BigInteger u, BigInteger S, BigInteger K)
        {
            Console.WriteLine("$[Client]");
            Console.WriteLine($"Scrambler (u): {u}");
            Console.WriteLine($"Session key (S): {S}");
            Console.WriteLine($"Encryption key (K): {K}");
            Console.WriteLine();
        }

        public static void DisplayConfirmationOnServerSide(BigInteger clientM, BigInteger serverM, BigInteger R)
        {
            Console.WriteLine("================ Confirmation ================");
            Console.WriteLine("$[Client]");
            Console.WriteLine($"Client M: {clientM}");
            Console.WriteLine("\nSending client M to server...\n");
            Console.WriteLine("$[Server]");
            Console.WriteLine($"Server M: {serverM}");
            Console.WriteLine($"Server R: {R}");
            Console.WriteLine("\nSending server R to client...");
            Console.WriteLine();
        }

        public static void DisplayConfirmationOnClientSide(BigInteger clientR)
        {
            Console.WriteLine("$[Client]");
            Console.WriteLine($"Client R: {clientR}");
            Console.WriteLine();
        }
    }
}