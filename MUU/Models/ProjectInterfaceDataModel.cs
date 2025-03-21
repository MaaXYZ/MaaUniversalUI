using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MUU.PIModels;

public class InterfaceData
{

    // [JsonPropertyName("version")]
    public string? version { get; set; }

    public string? message { get; set; }

    public required List<PiTask> task { get; set; }
}

public class PiTask
{
    public required string name { get; set; }
    public required string entry { get; set; }
}