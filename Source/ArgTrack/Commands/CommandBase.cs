using System;
using System.Windows.Input;

namespace ArgTrack.Commands
{
    public abstract class CommandBase<T> : ICommand where T : class
    {
        public abstract void Execute(T parameter);

        public virtual bool CanExecute(T parameter)
        {
            return false;
        }

        public void Execute(object parameter)
        {
            ValidateCommandType(parameter);

            Execute(parameter as T);
        }

        public bool CanExecute(object parameter)
        {
            ValidateCommandType(parameter);

            return CanExecute(parameter as T);
        }

        private void ValidateCommandType(object parameter)
        {
            if (parameter == null)
            {
                var type = typeof(T).Name;
                System.Diagnostics.Debug.WriteLine($"Attempted to parse Command parameter as {type} but was null");
                throw new ArgumentNullException(nameof(parameter));
            }

            if (!(parameter is T))
            {
                var type = typeof(T).Name;
                var actualType = parameter.GetType().Name;
                System.Diagnostics.Debug.WriteLine($"Attempted to parse Command parameter as {type} but was {actualType}");
                // TODO: correct type of exception?
                throw new ArgumentOutOfRangeException(nameof(parameter));
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    // TODO: transition all commands to CommandBase<T> :) nah, for some reason they don't always work...

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

        protected T GetParameterAs<T>(object parameter) where T : class
        {
            // TODO: should this throw?

            if (parameter == null)
            {
                var type = typeof(T).Name;
                System.Diagnostics.Debug.WriteLine($"Attempted to parse Command parameter as {type} but was null");
                return null;
            }

            if (!(parameter is T asParameter))
            {
                var type = typeof(T).Name;
                var actualType = parameter.GetType().Name;
                System.Diagnostics.Debug.WriteLine($"Attempted to parse Command parameter as {type} but was {actualType}");
                return null;
            }

            return asParameter;
        }


    //    protected object GetParameterAs<T1, T2>(object parameter) where T1 : class where T2 : class
    //    {
    //        return null;
    //    }
    }
}
