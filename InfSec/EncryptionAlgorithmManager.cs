namespace InfSec
{
    public abstract class EncryptionAlgorithmManager
    {
        protected EncryptionAlgorithmManager(InfSecSettings settings)
        {
            _settings = settings;
        }

        public abstract string Name { get; }
        public abstract void Execute();

        protected InfSecSettings _settings;
    }
}