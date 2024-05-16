using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notatnik_WPF;
internal class AddNoteDialogWindowViewModel : BaseViewModel, ICloseWindows
{
    public Repository Repository { get; set; } = Repository.Instance;

    public ICommand AddNoteCommand { get; set; }
    public Action Close { get; set; }

    public AddNoteDialogWindowViewModel()
    {
        AddNoteCommand = new RelayCommand(AddNote);
    }
    private void AddNote()
    {
        Repository.Notes.Add(new Note { Title = "New Note", Content = "New Content", EditTime = DateTime.Now });
        Close?.Invoke();
    }

    public bool CanClose()
    {
        return true;
    }
}