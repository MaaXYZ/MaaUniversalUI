using System.Text.Json.Serialization;

namespace MUU.Models;

public class ProjectInterfaceDataModel
{

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}