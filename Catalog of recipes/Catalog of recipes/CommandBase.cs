using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Catalog_of_recipes
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<object> action;
        private readonly Action _action;

        public CommandBase(Action<object> action)
        {
            this.action = action;
        }

        public CommandBase(Action action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action(parameter);

        }


    }
}
