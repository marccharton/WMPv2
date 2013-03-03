using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace SlideBarMVVM
{
    public class Command : ICommand
    {
        private Action _action;
        
        public bool CanExecute(object parameter)
        {
            return (true);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this._action();
        }

        public Command(Action a) 
        {
            _action = a;
        }
    }
}
