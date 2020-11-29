namespace InfSec
{
    public interface IEncryptionAlgorithmManager
    {
        public string Name { get; }
        public void Execute();
    }
}