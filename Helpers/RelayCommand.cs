using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notatnik_WPF;

internal class RelayCommand : ICommand
{
    private Action mAction;
    private Action<object> pAction;

    public RelayCommand(Action action)
    {
        mAction = action;
    }

    public RelayCommand(Action<object> pAction)
    {
        this.pAction = pAction;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if(parameter == null)
            mAction();
        else
            pAction(parameter);

    }
}
