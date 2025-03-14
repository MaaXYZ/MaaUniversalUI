using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using System;

using MUU.Models;
using MUU.Utils;

namespace MUU.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private ProjectInterfaceDataModel? _piData;
    public ProjectInterfaceDataModel? PiData { get => _piData; private set => this.RaiseAndSetIfChanged(ref _piData, value); }
    public MainWindowViewModel()
    {
        InitializeCommand = ReactiveCommand.CreateFromTask(LoadPiData);
        InitializeCommand.Execute().Subscribe(); // 立即执行命令
    }
    public ReactiveCommand<Unit, Unit> InitializeCommand { get; }

    private async Task LoadPiData()
    {
        PiData = await JsonFileReader.ReadAsync<ProjectInterfaceDataModel>("interface.json");
        Greeting = PiData?.Message;
    }

    public string _greeting = "Welcome to Avalonia!";
    public string Greeting { get => _greeting; private set => this.RaiseAndSetIfChanged(ref _greeting, value); }
}
