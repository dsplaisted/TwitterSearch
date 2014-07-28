using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MvvmPortable.Presentation.Input
{
    public abstract class Command : ICommand
    {
        bool _canExecute;

        protected Command()
        {
            _canExecute = true;
        }

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute()
        {
            return _canExecute;
        }

        public void SetCanExecute(bool canExecute)
        {
            if (_canExecute != canExecute)
            {
                _canExecute = canExecute;
                FireCanExecuteChanged();
            }
        }

        public abstract void Execute();

        protected void FireCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (parameter != null)
                throw new ArgumentException();  // Don't support parameters

            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            if (parameter != null)
                throw new ArgumentException();  // Don't support parameters

            Execute();
        }
    }
}
