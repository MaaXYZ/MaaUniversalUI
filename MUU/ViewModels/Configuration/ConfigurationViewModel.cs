using System.Reactive.Concurrency;
using ReactiveUI;

using MUU.PIModels;
using MUU.Utils;
using MUU.ConfModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MUU.ViewModels;

class ConfigurationViewModel : ViewModelBase
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
            RxApp.MainThreadScheduler.Schedule(SaveConfig);
        }
    }
    private Configuration _config = new Configuration
    {
        current = _defaultConfigName,
        allTaskConfig = new Dictionary<string, TaskConfig> { { _defaultConfigName, new TaskConfig { tasks = new List<ConfModels.ConfTask>(), } } },
        timer = new Dictionary<string, TimerConfig>(),
    };

    private const string _configFilename = "config/muu_config.json";
    private const string _defaultConfigName = "Default";
    private async Task LoadConfig()
    {
        Logger.log.Debug("Load Configuration: {filename}", _configFilename);
        var conf = await JsonFileSerializer.ReadAsync<Configuration>(_configFilename);
        Logger.log.Information("Config: {@data}", conf);

        if (conf == null)
        {
            // first time use
            Logger.log.Information("Config is null, first time use");
            SaveConfig();
            return;
        }
        _config = conf;
    }

    private async void SaveConfig()
    {
        Logger.log.Debug("Save Configuration: {filename}", _configFilename);
        await JsonFileSerializer.WriteAsync(_configFilename, Config);
    }


    public InterfaceData? PiData { get => _piData; private set => this.RaiseAndSetIfChanged(ref _piData, value); }
    private InterfaceData? _piData;

    private const string _piFilename = "interface.json";
    private async Task<bool> LoadPiData()
    {
        Logger.log.Debug("Load PiData: {filename}", _piFilename);
        PiData = await JsonFileSerializer.ReadAsync<InterfaceData>(_piFilename);
        Logger.log.Information("PiData: {@data}", PiData);

        if (PiData == null)
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