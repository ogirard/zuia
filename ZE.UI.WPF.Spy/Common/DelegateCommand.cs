using System;
using System.Windows.Input;

namespace ZE.UI.WPF.Spy.Common
{
  public class DelegateCommand : ICommand
  {
    private readonly Action _execute;

    private readonly Func<bool> _canExecute;

    public DelegateCommand(Action execute, Func<bool> canExecute = null)
    {
      _execute = execute;
      _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return _canExecute == null || _canExecute();
    }

    public void Execute(object parameter)
    {
      _execute();
    }

    public event EventHandler CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
      if (CanExecuteChanged != null)
      {
        CanExecuteChanged(this, EventArgs.Empty);
      }
    }
  }
}