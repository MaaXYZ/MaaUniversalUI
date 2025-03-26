using MUU.Utils;
using ReactiveUI;
using System;
using System.Collections.Generic;

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

    public MainWindowViewModel(TaskerViewModel taskerVM, ConnectionViewModel connectionVM, ConfigurationViewModel configurationVM, HelpViewModel helpVM, SettingsViewModel settingsVM)
    {
        _pages = new Dictionary<MainPage, object> {
            {MainPage.Tasker, taskerVM},
            {MainPage.Connection, connectionVM},
            {MainPage.Configuration, configurationVM},
            {MainPage.Help, helpVM},
            {MainPage.Settings, settingsVM},
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