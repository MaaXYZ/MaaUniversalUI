using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;

namespace MUU.ConfModels;

public class Configuration
{
    public required Dictionary<string, TaskConfig> allTask { get; set; }
    public required Dictionary<string, TimerConfig> timer { get; set; }
}

public class TimerConfig
{
    public required List<DayOfWeek> days { get; set; }
    public required List<int> hours { get; set; }

    public required List<string> tasks { get; set; }
}

public class TaskConfig
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