using ReactiveUI;

namespace MUU.ViewModels;

public class ConnectionViewModel : ViewModelBase
{
    public string _greeting = "Connection Page";
    public string Greeting { get => _greeting; private set => this.RaiseAndSetIfChanged(ref _greeting, value); }
}