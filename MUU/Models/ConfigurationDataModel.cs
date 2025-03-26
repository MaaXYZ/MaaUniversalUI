using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MUU.ConfModels;

public class Configuration
{
    public required string current { get; set; }
    public required Dictionary<string, TaskConfig> allTaskConfig { get; set; }
    public required Dictionary<string, TimerConfig> timer { get; set; }
}

public class TimerConfig
{
    public required List<DayOfWeek> days { get; set; }
    public required List<int> hours { get; set; }

    public required List<string> name { get; set; }
}

public class TaskConfig
{
    public required List<ConfTask> tasks { get; set; }
}

public class Option
{
    public required string name { get; set; }
    public required string value { get; set; }
}

public class ConfTask
{
    public required string piName { get; set; }
    public string customName { get; set; } = string.Empty;
    public bool enable { get; set; } = true;
    public List<Option>? option { get; set; }
}