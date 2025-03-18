using System.Reactive.Concurrency;
using System.Reactive;
using System.Collections.Generic;
using ReactiveUI;

using MUU.PIModels;
using MUU.Utils;
using Tmds.DBus.Protocol;
using Serilog;

namespace MUU.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        _pages = new Dictionary<string, object> {
            {"Tasker", new TaskerViewModel()},
            {"Connection", new ConnectionViewModel()},
            {"Configuration", new ConfigurationViewModel()},
            {"Help", new HelpViewModel()},
            {"Settings", new SettingsViewModel()},
        };
        _currentPage = _pages["Tasker"];

        NavigateCommand = ReactiveCommand.Create<string>(Navigate);
    }

    private Dictionary<string, object> _pages;

    private object _currentPage;

    public object CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }
    public ReactiveCommand<string, Unit> NavigateCommand { get; }


    private void Navigate(string page)
    {
        Logger.Log.Debug("onclick {page}", page);
        // CurrentPage = _pages[page];
    }
}
