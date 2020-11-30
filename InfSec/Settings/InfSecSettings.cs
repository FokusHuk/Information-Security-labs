using System.IO;
using Newtonsoft.Json;

namespace InfSec
{
    public class InfSecSettings
    {
        public InfSecSettings(CaesarSettings caesar, DHSettings dh, RSASettings rsa)
        {
            Caesar = caesar;
            DH = dh;
            RSA = rsa;
        }
        
        public static InfSecSettings FromJsonFile(string filePath)
        {
            var settingInJson = File.ReadAllText(filePath);

            var settings = JsonConvert.DeserializeObject<InfSecSettings>(settingInJson);

            return settings;
        }
        
        public CaesarSettings Caesar { get; }
        public DHSettings DH { get; }
        public RSASettings RSA { get; }
    }
}