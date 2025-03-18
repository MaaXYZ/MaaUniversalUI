using System;
using System.Collections.Generic;
using ReactiveUI;

using MUU.Utils;

namespace MUU.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public enum MainPage
    {
        Tasker,
        Connection,
        Configuration,
        Help,
        Settings
    }

    public MainWindowViewModel()
    {
        _pages = new Dictionary<MainPage, object> {
            {MainPage.Tasker, new TaskerViewModel()},
            {MainPage.Connection, new ConnectionViewModel()},
            {MainPage.Configuration, new ConfigurationViewModel()},
            {MainPage.Help, new HelpViewModel()},
            {MainPage.Settings, new SettingsViewModel()},
        };
        _currentPage = _pages[MainPage.Tasker];
    }

    private Dictionary<MainPage, object> _pages;

    private object _currentPage;

    public object CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    // only for Views
    public void NavigateByName(string pageName)
    {
        if (!Enum.TryParse(pageName, out MainPage page))
        {
            Logger.log.Error("Invalid page name: {pageName}", pageName);
            throw new ArgumentException("Invalid page name", nameof(pageName));
        }

        Navigate(page);
    }

    public void Navigate(MainPage page)
    {
        CurrentPage = _pages[page];
    }
}