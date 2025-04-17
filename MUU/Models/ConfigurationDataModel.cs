using System;
using System.Collections.Generic;

namespace MUU.Models;

public class Configuration
{
    public string current { get; set; }
    public Dictionary<string, TaskConfig> allTaskConfig { get; set; }
    public Dictionary<string, TimerConfig> timer { get; set; }
}

public class TimerConfig
{
    public List<DayOfWeek> days { get; set; }
    public List<int> hours { get; set; }

    public List<string> name { get; set; }
}

public class TaskConfig
{
    public List<ConfTask> tasks { get; set; }
}

public class Option
{
    public string name { get; set; }
    public string value { get; set; }
}

public class ConfTask
{
    public string piName { get; set; }
    public string customName { get; set; } = string.Empty;
    public bool enable { get; set; } = true;
    public List<Option>? option { get; set; }
}