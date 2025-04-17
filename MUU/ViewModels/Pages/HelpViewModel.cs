using ReactiveUI;

namespace MUU.ViewModels.Pages;

public class HelpViewModel : ViewModelBase
{
    public string _greeting = "Help Page";
    public string Greeting { get => _greeting; private set => this.RaiseAndSetIfChanged(ref _greeting, value); }
}