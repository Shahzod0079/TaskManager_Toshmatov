using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace TaskManager_Toshmatov.Classes
{
    public class RealyCommand : ICommand

    {
        private Action<object> execute;

        private Func<object, bool> canExecute;
        private Action<object> value;

        public RealyCommand(Action<object> value)
        {
            this.value = value;
        }

        public RealyCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.CanExecute(parameter);
        }
        public void Execute(object parameter) =>
            this.Execute(parameter);
    }
}