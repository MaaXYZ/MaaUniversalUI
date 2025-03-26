using ReactiveUI;

namespace MUU.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    public string _greeting = "Settings Page";
    public string Greeting { get => _greeting; private set => this.RaiseAndSetIfChanged(ref _greeting, value); }
}