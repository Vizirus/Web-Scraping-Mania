using System;
using System.Windows.Input;

namespace Web_Scraping_Mania.Commands
{
    public class CommandBase : ICommand
    {
        readonly Action<object> _action;
        readonly Predicate<object> _canExecute;
        public CommandBase(Action<object> action, Predicate<object> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute(parameter);
        }
        public void Execute(object? parameter)
        {
            _action(parameter);
        }
    }
}
