using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace Notatnik_WPF;

internal class NoteDialogWindowViewModel : BaseViewModel, ICloseWindows
{
    public ObservableCollection<Category> Categories { get; set; }
    public bool PrintTitle { get; set; } = true;
    public bool PrintDescription { get; set; } = true;
    public bool PrintCategory { get; set; } = true;
    public bool PrintTags { get; set; } = true;
    public ICommand PrintPreviewCommand { get; set; }
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
                if(tag == "") continue;
                Tags.Add(new Tag() { Name = tag });
            }

            tagsText = output;
        }
    }

    public bool IsDialogCanceled { get; set; } = true;
    public string Title { get; set; }
    public string Content { get; set; }
    public Category SelectedCategory { get; set; }
    public Note Note;

    public ICommand AddNoteCommand { get; set; }
    public Action Close { get; set; }

    public NoteDialogWindowViewModel(List<Category> categories)
    {
        PrintPreviewCommand = new RelayCommand(PrintPreview);
        Categories = new ObservableCollection<Category>(categories);

        AddNoteCommand = new RelayCommand(AddNote);
        if (categories.Count > 0)
            SelectedCategory = categories[0];

    }

    public NoteDialogWindowViewModel(List<Category> categories, Note note) : this(categories)
    {
        Title = note.Title;
        Content = note.Content;
        foreach(var category in categories)
        {
            if (category.Name == note.Category.Name)
            {
                SelectedCategory = category;
                break;
            }
        }
        string tags = "";
        if(note.Tags != null)
        foreach (var tag in note.Tags)
        {
            tags += tag.Name + " ";
        }
        TagsText = tags;
    }

    private void AddNote()
    {
        IsDialogCanceled = false;
        Note = new Note { Title = Title, Content = Content, EditTime = DateTime.Now, Category = SelectedCategory, Tags = Tags };
        Messenger.Send("SaveNote", Note);

        Close?.Invoke();
    }


    private FlowDocument GenerateFlowDocument()
    {
        FlowDocument doc = new FlowDocument();
        doc.PagePadding = new Thickness(50);
        doc.ColumnWidth = double.PositiveInfinity;

        // Create a title
        if (PrintTitle)
        {
            Paragraph title = new Paragraph(new Run("Title: " + Title))
            {
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center
            };
            doc.Blocks.Add(title);
            doc.Blocks.Add(new Paragraph(new LineBreak()));
        }

        // Create a description
        if (PrintDescription)
        {
            Paragraph description = new Paragraph(new Run("Description: " + Content))
            {
                FontSize = 16,
                FontWeight = FontWeights.Normal,
                TextAlignment = TextAlignment.Left
            };
            doc.Blocks.Add(description);
            doc.Blocks.Add(new Paragraph(new LineBreak()));
            doc.Blocks.Add(new BlockUIContainer(new Separator()));
        }

        // Create a category
        if (PrintCategory)
        {
            Paragraph category = new Paragraph(new Run("Category: " + SelectedCategory.Name))
            {
                FontSize = 16,
                FontWeight = FontWeights.Normal,
                TextAlignment = TextAlignment.Left
            };
            doc.Blocks.Add(category);
            doc.Blocks.Add(new Paragraph(new LineBreak()));
            doc.Blocks.Add(new BlockUIContainer(new Separator()));
        }

        // Create tags
        if (PrintTags)
        {
            string tags = string.Join(", ", Tags.Select(t => t.Name));
            Paragraph tagsParagraph = new Paragraph(new Run("Tags: " + tags))
            {
                FontSize = 16,
                FontWeight = FontWeights.Normal,
                TextAlignment = TextAlignment.Left
            };
            doc.Blocks.Add(tagsParagraph);
            doc.Blocks.Add(new Paragraph(new LineBreak()));
            doc.Blocks.Add(new BlockUIContainer(new Separator()));
        }

        return doc;
    }

    private void PrintPreview()
    {
        string xpsFilePath = "print_prev.xps";
        if (File.Exists(xpsFilePath))
            File.Delete(xpsFilePath);

        FlowDocument doc = GenerateFlowDocument();
        XpsDocument xpsDocument = new XpsDocument(xpsFilePath, FileAccess.ReadWrite);
        XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
        writer.Write(((IDocumentPaginatorSource)doc).DocumentPaginator);
        FixedDocumentSequence preview = xpsDocument.GetFixedDocumentSequence();
        xpsDocument.Close();

        var window = new Window
        {
            Title = "Print Preview",
            Content = new DocumentViewer { Document = preview },
            Width = 900,
            Height = 700
        };
        window.ShowDialog();

        writer = null;
        xpsDocument = null;
        preview = null;
    }
    public bool CanClose()
    {
        return true;
    }



}