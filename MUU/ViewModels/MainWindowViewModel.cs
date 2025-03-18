using System.Reactive.Concurrency;
using ReactiveUI;

using MUU.PIModels;
using MUU.Utils;
using Serilog;
using System;

namespace MUU.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public InterfaceDataModel? PiData { get => _piData; private set => this.RaiseAndSetIfChanged(ref _piData, value); }
    private InterfaceDataModel? _piData;

    public MainWindowViewModel()
    {
        RxApp.MainThreadScheduler.Schedule(LoadPiData);
    }

    private const string ProjectInterfaceFilename = "interface.json";
    private async void LoadPiData()
    {
        Logger.Log.Debug("Load PiData: {filename}", ProjectInterfaceFilename);
        PiData = await JsonFileReader.ReadAsync<InterfaceDataModel>(ProjectInterfaceFilename);
        Logger.Log.Information("PiData: {@data}", PiData);

        UpdateView();
    }

    private void UpdateView()
    {
        if (PiData == null)
        {
            Logger.Log.Error("PiData is null");
            return;
        }

        if (!string.IsNullOrEmpty(PiData.message))
        {
            Greeting = PiData.message;
        }
    }

    public string _greeting = "Welcome to Avalonia!";
    public string Greeting { get => _greeting; private set => this.RaiseAndSetIfChanged(ref _greeting, value); }
}
