using System;
using System.Windows.Input;

namespace AkiraBot.UI.Core;

public sealed class BaseCommand : ICommand
{
    private readonly Action<object> _method;
    private readonly Predicate<object>? _canExecute;

    public BaseCommand(Action<object> method, Predicate<object> canExecute = null)
    {
        _method = method;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true;
    }

    public void Execute(object parameter)
    {
        _method.Invoke(parameter);
    }
}