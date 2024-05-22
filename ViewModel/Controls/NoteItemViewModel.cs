using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notatnik_WPF;
internal class NoteItemViewModel : BaseViewModel
{
    public ICommand OpenNoteCommand { get; set; }
    public ICommand DeleteNoteCommand { get; set; }
    public NoteItemViewModel(Note note)
    {
        Title = note.Title;
        Content = note.Content;
        OpenNoteCommand = new RelayCommand(OpenNote);
        DeleteNoteCommand = new RelayCommand(DeleteNote);
    }
    void OpenNote()
    {
        Messenger.Send("OpenNote", Title);
    }
    void DeleteNote()
    {
        Messenger.Send("DeleteNote", Title);
    }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime EditDate { get; set; }


}
