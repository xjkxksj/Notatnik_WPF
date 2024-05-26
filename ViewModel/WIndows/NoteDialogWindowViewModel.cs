using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notatnik_WPF;

internal class NoteDialogWindowViewModel : BaseViewModel, ICloseWindows
{
    public ObservableCollection<Category> Categories { get; set; }

    List<Tag> Tags;
    private string tagsText;
    public string TagsText
    {
        get { return tagsText; }
        set
        {
            string cleanedInput = Regex.Replace(value, "[ ,;.$%^*()]+", " ");
            string[] parts = cleanedInput.Split(' ');

            // Remove duplicates
            parts = parts.Distinct().ToArray();

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Length > 0 && !parts[i].StartsWith("#"))
                    parts[i] = "#" + parts[i];
            }

            string output = string.Join(" ", parts);

            Tags = new List<Tag>();
            foreach (var tag in parts)
            {
                Tags.Add(new Tag() { Name = tag });
            }

            tagsText = output;
        }
    }

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
        Note = new Note { Title = Title, Content = Content, EditTime = DateTime.Now, Category = SelectedCategory, Tags = Tags };
        Messenger.Send("SaveNote", Note);


        Close?.Invoke();
    }

    public bool CanClose()
    {
        return true;
    }



}