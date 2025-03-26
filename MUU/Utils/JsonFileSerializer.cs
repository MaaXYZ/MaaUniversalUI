using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MUU.Utils;

public static class JsonFileSerializer
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

        await using var stream = File.OpenRead(filePath);
        return await JsonSerializer.DeserializeAsync<T>(stream);
    }

    public static async Task WriteAsync<T>(string filePath, T value)
    {
        var dir = Path.GetDirectoryName(filePath);
        if (dir != null && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        await using FileStream createStream = File.Create(filePath);
        await JsonSerializer.SerializeAsync(createStream, value);
    }
}