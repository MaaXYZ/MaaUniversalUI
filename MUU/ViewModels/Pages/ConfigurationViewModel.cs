using MUU.Models;
using MUU.Utils;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Threading.Tasks;

namespace MUU.ViewModels.Pages;

public class CurrentTaskConfigChanged
{
    // public required TaskConfig oldConfig;

    public string configName;
    public TaskConfig newConfig;
}

public class ConfigurationViewModel : ViewModelBase
{
    public ConfigurationViewModel()
    {
        RxApp.MainThreadScheduler.Schedule(LoadAndCheckData);
    }

    public TaskConfig CurrentTaskConfig()
    {
        return Config.allTaskConfig[Config.current];
    }

    public Configuration Config
    {
        get => _config; private set
        {
            this.RaiseAndSetIfChanged(ref _config, value);

            NotifyCurrentTaskConfigChanged();

            RxApp.MainThreadScheduler.Schedule(SaveConfig);
        }
    }

    private void NotifyCurrentTaskConfigChanged()
    {
        MessageBus.Current.SendMessage(new CurrentTaskConfigChanged { configName = Config.current, newConfig = CurrentTaskConfig() });
    }

    private Configuration _config = new Configuration
    {
        current = _defaultConfigName,
        allTaskConfig = new Dictionary<string, TaskConfig> { { _defaultConfigName, new TaskConfig { tasks = [], } } },
        timer = [],
    };

    private const string _configFilename = "config/muu_config.json";
    private const string _defaultConfigName = "Default";

    private async Task LoadConfig()
    {
        Logger.log.Debug("Load Configuration: {filename}", _configFilename);
        var conf = await JsonFileSerializer.ReadAsync<Configuration>(_configFilename);
        Logger.log.Information("Config: {@data}", conf);

        if (conf == null) // first time use
        {
            Logger.log.Information("Config is null, first time use");
            Config = _config;
        }
        else
        {
            Config = conf;
        }
    }

    private async void SaveConfig()
    {
        Logger.log.Debug("Save Configuration: {filename}", _configFilename);
        await JsonFileSerializer.WriteAsync(_configFilename, Config);
    }

    public InterfaceData? PiData { get => _piData; }
    private InterfaceData? _piData;

    private const string _piFilename = "interface.json";

    private async Task<bool> LoadPiData()
    {
        Logger.log.Debug("Load PiData: {filename}", _piFilename);
        _piData = await JsonFileSerializer.ReadAsync<InterfaceData>(_piFilename);
        Logger.log.Information("PiData: {@data}", _piData);

        if (_piData == null)
        {
            Logger.log.Error("PiData is null");
            Greeting = $"Failed to load {_piFilename}";
            return false;
        }

        return true;
    }

    private async void LoadAndCheckData()
    {
        await LoadPiData();
        await LoadConfig();
    }

    public string _greeting = "Configuration Page";
    public string Greeting { get => _greeting; private set => this.RaiseAndSetIfChanged(ref _greeting, value); }
}