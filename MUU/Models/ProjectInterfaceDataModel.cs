using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MUU.PIModels;

public class InterfaceData
{

    // [JsonPropertyName("version")]
    public string? version { get; set; }

    public string? message { get; set; }

    public required List<Task> task { get; set; }
}

public class Task
{
    public required string name { get; set; }
    public required string entry { get; set; }
}