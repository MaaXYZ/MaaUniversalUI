using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MUU.ConfModels;

public class Configuration
{
    public required List<Task> task { get; set; }
}

public class Option
{
    public required string name { get; set; }
    public required string value { get; set; }
}

public class Task
{
    public required string name { get; set; }
    public List<Option>? option { get; set; }
}