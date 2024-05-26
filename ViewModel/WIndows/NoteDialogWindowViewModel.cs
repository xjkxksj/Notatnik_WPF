using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notatnik_WPF;

internal class NoteDialogWindowViewModel : BaseViewModel, ICloseWindows
{
    public ObservableCollection<Category> Categories { get; set; }



    public string Title { get; set; }
    public string Content { get; set; }
    public Category SelectedCategory { get; set; }
    public Note Note;

    public ICommand AddNoteCommand { get; set; }
    public Action Close { get; set; }

    public NoteDialogWindowViewModel(List<Category> categories)
    {
        Categories = new ObservableCollection<Category>(categories);

        AddNoteCommand = new RelayCommand(AddNote);
        if (categories.Count > 0)
            SelectedCategory = categories[0];
    }

    public NoteDialogWindowViewModel(List<Category> categories, Note note) : this(categories)
    {
        Title = note.Title;
        Content = note.Content;
        SelectedCategory = note.Category;
    }

    private void AddNote()
    {
        Note = new Note { Title = Title, Content = Content, EditTime = DateTime.Now, Category = SelectedCategory };
        Messenger.Send("SaveNote", Note);


        Close?.Invoke();
    }

    public bool CanClose()
    {
        return true;
    }



}