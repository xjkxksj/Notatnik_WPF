
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notatnik_WPF;
internal class NoteItemViewModel : BaseViewModel
{
    public ICommand OpenNote { get; set; }
    public ICommand DeleteNote { get; set; }
    public NoteItemViewModel(Note note)
    {
        Title = note.Title;
        Content = note.Content;
        OpenNote = new RelayCommand(OpenEditNote);
        DeleteNote = new RelayCommand(OpenEditNote2);
    }
    void OpenEditNote()
    {
        Messenger.Send("OpenNote", Title);
    }
    void OpenEditNote2()
    {
        Messenger.Send("OpenNote", "DUPA");
    }
    public string Title { get; set; }

    public string Content { get; set; }
    public DateTime EditDate { get; set; }


}
