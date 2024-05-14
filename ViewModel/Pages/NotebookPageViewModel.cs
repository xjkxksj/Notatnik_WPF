using Microsoft.Win32;
using Notatnik_WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notatnik_WPF;
internal class NotebookPageViewModel
{
    public ICommand AddNoteCommand { get; set; }

    public NotebookPageViewModel()
    {
        AddNoteCommand = new RelayCommand(AddNote);
    
    }
    public void AddNote()
    {
        AddNoteDialogWindow addNoteDialogWindow = new AddNoteDialogWindow();
        addNoteDialogWindow.ShowDialog();
    }
}
