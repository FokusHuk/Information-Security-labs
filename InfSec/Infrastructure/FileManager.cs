using System.IO;

namespace InfSec.Infrastructure
{
    public static class FileManager
    {
        public static string GetTextFromFile(string path)
        {
            var reader = new StreamReader(path);
            var text = reader.ReadToEnd();
            reader.Close();
            return text;
        }
    }
}