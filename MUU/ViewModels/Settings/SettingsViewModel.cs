using System.Reactive.Concurrency;
using ReactiveUI;

using MUU.PIModels;
using MUU.Utils;
using System.Reactive;

namespace MUU.ViewModels;

class SettingsViewModel : ViewModelBase
{
    public string _greeting = "Settings Page";
    public string Greeting { get => _greeting; private set => this.RaiseAndSetIfChanged(ref _greeting, value); }
}