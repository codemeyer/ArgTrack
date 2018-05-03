using System;
using System.Windows.Input;

namespace ArgTrack.Commands
{
    public abstract class CommandBase : ICommand
    {
        public abstract void Execute(object parameter);

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
