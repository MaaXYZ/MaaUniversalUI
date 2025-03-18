using System.Reactive.Concurrency;
using ReactiveUI;

using MUU.PIModels;
using MUU.Utils;
using System.Reactive;

namespace MUU.ViewModels;

class TaskerViewModel : ViewModelBase
{
    public TaskerViewModel()
    {
        RxApp.MainThreadScheduler.Schedule(LoadPiData);
    }

    public InterfaceDataModel? PiData { get => _piData; private set => this.RaiseAndSetIfChanged(ref _piData, value); }
    private InterfaceDataModel? _piData;

    private const string ProjectInterfaceFilename = "interface.json";
    private async void LoadPiData()
    {
        Logger.log.Debug("Load PiData: {filename}", ProjectInterfaceFilename);
        PiData = await JsonFileReader.ReadAsync<InterfaceDataModel>(ProjectInterfaceFilename);
        Logger.log.Information("PiData: {@data}", PiData);

        if (PiData == null)
        {
            Logger.log.Error("PiData is null");
            return;
        }

        if (!string.IsNullOrEmpty(PiData.message))
        {
            Greeting = PiData.message;
        }
    }

    public string _greeting = "Tasker Page";
    public string Greeting { get => _greeting; private set => this.RaiseAndSetIfChanged(ref _greeting, value); }
}