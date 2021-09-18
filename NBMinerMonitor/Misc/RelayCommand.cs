using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace NBMinerMonitor.Misc
{
    /// <summary>
    /// Базовая реализация команды для MVVM
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action<object> executedAction;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> action)
        {
            executedAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter = null)
        {
            executedAction?.Invoke(parameter);
        }
    }
}
