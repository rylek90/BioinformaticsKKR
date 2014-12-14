using System;
using System.Windows.Input;

namespace BioinformaticsKKR.Core.ViewModel
{
    public class CommandBase : ICommand
    {
        public Predicate<object> CanExecuteMethod { get; set; }
        public Action<object> ExecuteMethod { get; set; }

        public bool CanExecute(object parameter)
        {
            return CanExecuteMethod(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void UpdateCanExecuteState()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            ExecuteMethod(parameter);
        }
    }
}