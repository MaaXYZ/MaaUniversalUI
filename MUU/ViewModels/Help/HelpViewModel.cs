using System.Reactive.Concurrency;
using ReactiveUI;

using MUU.PIModels;
using MUU.Utils;
using System.Reactive;

namespace MUU.ViewModels;

class HelpViewModel : ViewModelBase
{
    public string _greeting = "Help Page";
    public string Greeting { get => _greeting; private set => this.RaiseAndSetIfChanged(ref _greeting, value); }
}