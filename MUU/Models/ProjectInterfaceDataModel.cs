using System.Collections.Generic;

namespace MUU.Models;

public class InterfaceData
{
    // [JsonPropertyName("version")]
    public string? version { get; set; }

    public string? message { get; set; }

    public List<PiTask> task { get; set; }
}

public class PiTask
{
    public string name { get; set; }
    public string entry { get; set; }
}