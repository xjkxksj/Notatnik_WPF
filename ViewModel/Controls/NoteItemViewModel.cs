using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notatnik_WPF;
internal class NoteItemViewModel : BaseViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime EditDate { get; set; }
    public Category Category { get; set; }
    public List<Tag> Tags { get; set; }
    public ICommand OpenNoteCommand { get; set; }
    public ICommand DeleteNoteCommand { get; set; }
    public NoteItemViewModel(Note note)
    {
        Title = note.Title;
        Content = note.Content;
        Category = note.Category;
        Tags = new List<Tag>();

        //Dummy Tags
        for (int i = 0; i < 15; i++)
        {
            Tags.Add(new Tag() { Name = $"Tag{i}"});
        }

        OpenNoteCommand = new RelayCommand(OpenNote);
        DeleteNoteCommand = new RelayCommand(DeleteNote);
    }
    public List<Tag> VisibleTags { get; set; }

    public bool AreMoreTags
    {
        get { return Tags.Count > VisibleTags.Count; }
    }

    public void UpdateVisibleTags(double availableWidth)
    {
        double tagWidth = 70;
        int maxTags = (int)(availableWidth / tagWidth);

        VisibleTags = Tags.Take(maxTags).ToList();
    }

    void OpenNote()
    {
        Messenger.Send("OpenNote", Title);
    }
    void DeleteNote()
    {
        Messenger.Send("DeleteNote", Title);
    }
}
