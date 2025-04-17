using System.Reactive.Concurrency;
using ReactiveUI;

using MUU.Models;
using MUU.Utils;
using System.Reactive;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MUU.ViewModels.Pages;

public class TaskerViewModel : ViewModelBase, IDisposable
{
    private IDisposable _subscription;

    public TaskerViewModel()
    {
        _subscription = MessageBus.Current.Listen<CurrentTaskConfigChanged>().Subscribe(msg => OnCurrentTaskConfigChanged(msg.newConfig));
    }

    public void Dispose()
    {
        _subscription?.Dispose(); // 防止内存泄漏
    }

    private void OnCurrentTaskConfigChanged(TaskConfig taskConfig)
    {
        Logger.log.Information("taskConfig: {@taskConfig}", taskConfig);
        TaskItems = [.. from a in taskConfig.tasks
                        select new TaskItem
                        {
                            Name = a.piName,
                        }];
        Logger.log.Debug("TaskItems: {@TaskItems}", TaskItems);
    }

    public class TaskItem
    {
        public string Name { get; set; }
        public bool IsRunning { get; set; } = false;
    }

    public ObservableCollection<TaskItem> TaskItems { get; set; } = [];
}