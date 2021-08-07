using System.IO;
using System.Text.Json;

namespace Project
{
    public static class JsonFileReader
    {
        public static T Read<T>(string filePath)
        {
            var text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text);
        }
    }
}