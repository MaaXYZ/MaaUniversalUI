using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MUU.ConfModels;

public class ConfigurationDataModel
{

    // [JsonPropertyName("version")]
    public string? version { get; set; }

    public string? message { get; set; }

    public required List<TaskDataModel> task { get; set; }
}

public class TaskDataModel
{
    public required string name { get; set; }
    public required string entry { get; set; }
}