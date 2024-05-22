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
    public ObservableCollection<Category> Categories { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int SelectedCategoryId { get; set; }
    public Note Note;

    public ICommand AddNoteCommand { get; set; }
    public Action Close { get; set; }

    public AddNoteDialogWindowViewModel(List<Category> categories)
    {
        Categories = new ObservableCollection<Category>(categories);
        AddNoteCommand = new RelayCommand(AddNote);
    }

    public AddNoteDialogWindowViewModel(List<Category> categories, Note note) : this(categories)
    {
        Title = note.Title;
        Content = note.Content;
        SelectedCategoryId = Categories.IndexOf(note.Category);
    }

    private void AddNote()
    {
        Note = new Note { Title = Title, Content = Content, EditTime = DateTime.Now , Category = Categories[SelectedCategoryId] };
        Messenger.Send("SaveNote", Note);
        Close?.Invoke();
    }

    public bool CanClose()
    {
        return true;
    }
}