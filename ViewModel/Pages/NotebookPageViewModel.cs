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
    public ObservableCollection<Note> Notes { get; set; }
    public Repository repository = new Repository();

    public ICommand AddNoteCommand { get; set; }

    public NotebookPageViewModel()
    {
        repository.loadFromFile("Repo");
        AddNoteCommand = new RelayCommand(AddNote);
        Notes = new ObservableCollection<Note>(repository.Notes);

        Messenger.Subscribe<string>("CreateCategory", CreateCategory);
        Messenger.Send("CategoryChanged", repository.Categories);


    }
    private void CreateCategory(string categoryName)
    {
        Console.WriteLine("CreateCategory");
        Category category = new Category { Name = categoryName };
        repository.Categories.Add(category);
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
        Notes.Add(note);
    }



}
