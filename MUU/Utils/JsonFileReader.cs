using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace MUU.Utils;

public static class JsonFileReader
{
    public static async Task<T?> ReadAsync<T>(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return default;
        }

        if (!File.Exists(filePath))
        {
            return default;
        }

        using FileStream stream = File.OpenRead(filePath);
        return await JsonSerializer.DeserializeAsync<T>(stream);
    }
}