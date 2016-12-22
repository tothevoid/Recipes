using System;
using System.Windows.Input;

namespace Catalog_of_recipes
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<object> action;
      

        public CommandBase(Action<object> action)
        {
            this.action = action;
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
