using System.Reactive.Concurrency;
using ReactiveUI;

using MUU.PIModels;
using MUU.Utils;

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
        Logger.log.Debug("Load PiData {@ProjectInterfaceFilename}", ProjectInterfaceFilename);

        PiData = await JsonFileReader.ReadAsync<InterfaceDataModel>(ProjectInterfaceFilename);
        if (PiData == null)
        {
            Logger.log.Error("Falied to load PiData");
            return;
        }
        Logger.log.Information("PiData: {@PiData}", PiData);

        if (!string.IsNullOrEmpty(PiData.message))
        {
            Greeting = PiData.message;
        }
    }

    public string _greeting = "Welcome to Avalonia!";
    public string Greeting { get => _greeting; private set => this.RaiseAndSetIfChanged(ref _greeting, value); }
}
