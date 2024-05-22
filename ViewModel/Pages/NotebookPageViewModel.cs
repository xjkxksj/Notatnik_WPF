using Microsoft.Win32;
using Notatnik_WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notatnik_WPF;
internal class NotebookPageViewModel
{
    public ObservableCollection<NoteItemViewModel> Notes { get; set; }

    public Repository repository = new Repository();

    NoteItemViewModel openedNote;

    public ICommand AddNoteCommand { get; set; }

    public NotebookPageViewModel()
    {
        //repository.loadFromFile("RepoNotes.txt");
        //repository.loadFromFile("RepoCategories.txt");
        repository.loadFromFile("Repo.txt");
        AddNoteCommand = new RelayCommand(AddNote);
        Notes = new ObservableCollection<NoteItemViewModel>();
        foreach (Note note in repository.Notes)
        {
            Notes.Add(new NoteItemViewModel(note));
        }

        Messenger.Subscribe<string>("CreateCategory", CreateCategory);
        Messenger.Subscribe<string>("OpenNote", OpenNote);
        Messenger.Subscribe<string>("DeleteNote", DeleteNote);
        Messenger.Subscribe<Note>("SaveNote", SaveNote);
        Messenger.Send("CategoryChanged", repository.Categories);


    }

    private void DeleteNote(string title)
    {
        Note note = repository.Notes.FirstOrDefault(n => n.Title == title);
        NoteItemViewModel vm = Notes.FirstOrDefault(n => n.Title == title);

        repository.Notes.Remove(note);
        repository.saveToFile("Repo.txt");

        Notes.Remove(vm);
    }
    private void OpenNote(string title)
    {

        Note note = repository.Notes.FirstOrDefault(n => n.Title == title);
        NoteItemViewModel vm = Notes.FirstOrDefault(n => n.Title == title);

        openedNote = vm;
        repository.Notes.Remove(note);

        AddNoteDialogWindowViewModel addNoteVM = new AddNoteDialogWindowViewModel(repository.Categories, note);
        AddNoteDialogWindow addNoteView = new AddNoteDialogWindow();

        addNoteView.DataContext = addNoteVM;
        addNoteView.ShowDialog();
    }
    private void CreateCategory(string categoryName)
    {
        Category category = new Category { Name = categoryName };
        repository.Categories.Add(category);
        //repository.saveToFile("RepoCategories.txt", "Categories");
        repository.saveToFile("Repo.txt");
        Messenger.Send("CategoryChanged", repository.Categories);
    }
    private void AddNote()
    {
        AddNoteDialogWindowViewModel addNoteVM = new AddNoteDialogWindowViewModel(repository.Categories);
        AddNoteDialogWindow addNoteView = new AddNoteDialogWindow();

        addNoteView.DataContext = addNoteVM;
        addNoteView.ShowDialog();
    }

    private void SaveNote(Note note)
    {
        if(repository.Notes.FirstOrDefault(n => n.Title == note.Title) != null)
        {
            return;
        }
        if (openedNote != null)
        {
            Notes.Remove(openedNote);
            openedNote = null;
        }

        repository.Notes.Add(note);
        repository.saveToFile("Repo.txt");
        Notes.Add(new NoteItemViewModel(note));
    }



}
