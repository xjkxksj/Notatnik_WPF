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

    public ICommand AddNoteCommand { get; set; }

    public NotebookPageViewModel()
    {
        //repository.loadFromFile("RepoNotes.txt");
        //repository.loadFromFile("RepoCategories.txt");
        repository.loadFromFile("Repo.txt");
        AddNoteCommand = new RelayCommand(AddNote);
        Notes = new ObservableCollection<NoteItemViewModel>();

        Messenger.Subscribe<string>("CreateCategory", CreateCategory);
        Messenger.Subscribe<string>("OpenNote", OpenNote);
        Messenger.Send("CategoryChanged", repository.Categories);


    }
    private void OpenNote(string title)
    {
        
        Note note = repository.Notes.FirstOrDefault(n => n.Title == title);
        if (note == null)
            throw new Exception("Tried to open note that is not in repository");

        repository.Notes.Remove(note); 

        AddNoteDialogWindowViewModel addNoteVM = new AddNoteDialogWindowViewModel(repository.Categories, note);
        AddNoteDialogWindow addNoteView = new AddNoteDialogWindow();


        addNoteVM.Close = () =>
        {
            SaveNote(addNoteVM.Note);
        };

        addNoteView.DataContext = addNoteVM;
        addNoteView.ShowDialog();
    }
    private void CreateCategory(string categoryName)
    {
        Console.WriteLine("CreateCategory");
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

   
        addNoteVM.Close = () => 
        {
            SaveNote(addNoteVM.Note);
        };

        addNoteView.DataContext = addNoteVM;
        addNoteView.ShowDialog();
    }

    private void SaveNote(Note note)
    {
        repository.Notes.Add(note);
        //repository.saveToFile("RepoNotes.txt", "Notes");
        repository.saveToFile("Repo.txt");
        Notes.Add(new NoteItemViewModel(note));
    }



}
