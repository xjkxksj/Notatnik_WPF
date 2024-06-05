using Microsoft.Win32;
using Notatnik_WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Notatnik_WPF;
internal class NotebookPageViewModel
{
    public ObservableCollection<NoteItemViewModel> Notes { get; set; }

    public Repository repository = new Repository();

    NoteItemViewModel openedNote;

    public ICollectionView FilteredNotes { get; set; }
    public ICommand AddNoteCommand { get; set; }
    public Array SortingTypes { get; set; } = Enum.GetValues(typeof(SortingType));
    private SortingType selectedSortingType;
    public SortingType SelectedSortingType
    {
        get { return selectedSortingType; }
        set
        {
            selectedSortingType = value;
            SetDateSorting(value);
        }
    }


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

        FilteredNotes = CollectionViewSource.GetDefaultView(Notes);
        FilteredNotes.Filter = (_) => true;

        Messenger.Subscribe<string>("CreateCategory", CreateCategory);
        Messenger.Subscribe<string>("OpenNote", OpenNote);
        Messenger.Subscribe<string>("DeleteNote", DeleteNote);
        Messenger.Subscribe<Note>("SaveNote", SaveNote);
        Messenger.Subscribe<FilterValues>("SetFilters", SetFilters);
        Messenger.Subscribe<Category>("CategoryDeleted", DeleteCategory);
        Messenger.Send("CategoryChanged", repository.Categories);


    }

    private void SetFilters(FilterValues filterValues)
    {
        FilteredNotes.Filter = (note) =>
        {
            NoteItemViewModel noteViewModel = note as NoteItemViewModel;
            if (noteViewModel == null)
            {
                return true;
            }

            if (filterValues.FromDate != null && noteViewModel.EditDate < filterValues.FromDate)
            {
                return false;
            }

            if (filterValues.ToDate != null && noteViewModel.EditDate > filterValues.ToDate)
            {
                return false;
            }

            if ((filterValues.Category != null && filterValues.Category.Name != repository.Categories[0].Name) && (noteViewModel.Category == null || noteViewModel.Category.Name != filterValues.Category.Name))
            {
                return false;
            }

            if (filterValues.SearchText != null && (noteViewModel.Title == null || !noteViewModel.Title.Contains(filterValues.SearchText)))
            {
                return false;
            }

            if (filterValues.Tags != null && filterValues.Tags.Count > 0)
            {
                if (noteViewModel.Tags == null)
                {
                    return false;
                }

                foreach (var tag in filterValues.Tags)
                {
                    if (!noteViewModel.Tags.Any(t => t.Name.Contains(tag.Name)))
                    {
                        return false;
                    }
                }
            }

            return true;
        };
    }

    private void SetDateSorting(SortingType sorting)
    {
        FilteredNotes.SortDescriptions.Clear();
        switch (sorting)
        {
            case SortingType.None:
                break;
            case SortingType.Ascending:
                FilteredNotes.SortDescriptions.Clear();
                FilteredNotes.SortDescriptions.Add(new SortDescription("EditDate", ListSortDirection.Ascending));
                break;
            case SortingType.Descending:
                FilteredNotes.SortDescriptions.Clear();
                FilteredNotes.SortDescriptions.Add(new SortDescription("EditDate", ListSortDirection.Descending));
                break;
        }

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

        NoteDialogWindowViewModel addNoteVM = new NoteDialogWindowViewModel(repository.Categories, note);
        NoteDialogWindow addNoteView = new NoteDialogWindow();

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
        NoteDialogWindowViewModel addNoteVM = new NoteDialogWindowViewModel(repository.Categories);
        NoteDialogWindow addNoteView = new NoteDialogWindow();

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

        FilteredNotes.Refresh();
    }

    private void DeleteCategory(Category category)
    {
        repository.Categories.Remove(category);
        Messenger.Send("CategoryChanged", repository.Categories);
        for(int i = 0; i < repository.Notes.Count; i++)
        {
            if (repository.Notes[i].Category == category)
            {
                repository.Notes[i].Category = null;

            }
        }
        Notes.Clear();
        foreach (Note note in repository.Notes)
        {
            Notes.Add(new NoteItemViewModel(note));
        }
        
        repository.saveToFile("Repo.txt");

    }



}
