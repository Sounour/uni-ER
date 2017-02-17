using System;
using System.Windows.Input;

namespace WorldDisplay.ViewModel
{
    public class CommandExecution<T> : ICommand
    {
        private readonly Action<T> action;
        private readonly Func<T, bool> canExecute;

        public CommandExecution(Action<T> action, Func<T, bool> func)
        {
            this.action = action;
            canExecute = func;
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            return (canExecute != null) && (parameter is T ? canExecute((T) parameter) : canExecute(default(T)));
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                if (parameter != null) action.Invoke((T) parameter);
                else action.Invoke(default(T));
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}