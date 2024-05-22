using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Notatnik_WPF;
internal class Repository
{
    public static Repository Instance { get; } = new Repository();
    public List<Note> Notes { get; set; } = new List<Note>();
    public List<Category> Categories { get; set; } = new List<Category>();
    public List<Tag> Tags { get; set; }  = new List<Tag>();

    public void loadFromFile(string fileName)
    {
        Notes.Clear();
        Categories.Clear();
        Tags.Clear();
        int emptyLinesCount = 0;
        if (!File.Exists(fileName))
            return;
        foreach (string line in File.ReadLines(fileName))
        {
            if (line.Length > 1)
            {
                if (line.Contains("Title"))
                    Notes.Add(JsonSerializer.Deserialize<Note>(line));
                else if (line.Contains("Name") && emptyLinesCount == 1)
                    Categories.Add(JsonSerializer.Deserialize<Category>(line));
                else if (line.Contains("Name") && emptyLinesCount == 2)
                    Tags.Add(JsonSerializer.Deserialize<Tag>(line));
            }
            else { emptyLinesCount++; }
        }
    }
    public void saveToFile(string fileName)
    {
        File.WriteAllText(fileName, string.Empty);
        foreach (var note in Notes)
        {
            File.AppendAllLines(fileName, [JsonSerializer.Serialize(note)]);
        }
        File.AppendAllText(fileName, "\n");
        foreach (var category in Categories)
        {
            File.AppendAllLines(fileName, [JsonSerializer.Serialize(category)]);
        }
        File.AppendAllText(fileName, "\n");
        foreach (var tag in Tags)
        {
            File.AppendAllLines(fileName, [JsonSerializer.Serialize(tag)]);
        }
    }
    //public void loadFromFile(string fileName)
    //{
    //    Notes.Clear();
    //    Categories.Clear();
    //    Tags.Clear();
    //    string firstLine = null;
    //    if(!File.Exists(fileName))
    //        return;
    //    foreach (string line in File.ReadLines(fileName))
    //    {
    //        if (firstLine == null)
    //        {
    //            firstLine = line;
    //            Console.WriteLine(firstLine);
    //            continue;
    //        }
    //        if (firstLine.Contains("Notes"))
    //        {
    //            Notes.Add(JsonSerializer.Deserialize<Note>(line));
    //            Console.WriteLine(Notes[0].Title);
    //        }
    //        else if (firstLine.Contains("Categories"))
    //            Categories.Add(JsonSerializer.Deserialize<Category>(line));
    //        else if (firstLine.Contains("Tags"))
    //            Tags.Add(JsonSerializer.Deserialize<Tag>(line));
    //    }
    //}
    //public void saveToFile(string fileName, string dataType)
    //{
    //    File.WriteAllText(fileName, $"{dataType}:\n");
    //    if (dataType == "Notes")
    //    {
    //        foreach (var note in Notes)
    //        {
    //            File.AppendAllLines(fileName, [JsonSerializer.Serialize(note)]);
    //        }
    //    }
    //    else if (dataType == "Categories")
    //    {
    //        foreach (var category in Categories)
    //        {
    //            File.AppendAllLines(fileName, [JsonSerializer.Serialize(category)]);
    //        }
    //    }
    //    else if (dataType == "Tags")
    //    {
    //        foreach (var tag in Tags)
    //        {
    //            File.AppendAllLines(fileName, [JsonSerializer.Serialize(tag)]);
    //        }
    //    }

    //}
    public Repository()
    {
        //Dummy data
        Categories.Add(new Category { Name = "Category1" });
        Categories.Add(new Category { Name = "Category2" });
        Categories.Add(new Category { Name = "Category3" });
        Tags.Add(new Tag { Name = "Tag1" });
        Tags.Add(new Tag { Name = "Tag2" });
        Tags.Add(new Tag { Name = "Tag3" });
        Notes.Add(new Note { Title = "Note1", Content = "Content1", EditTime = DateTime.Now });
        Notes.Add(new Note { Title = "Note2", Content = "Content2", EditTime = DateTime.Now });
        Notes.Add(new Note { Title = "Note3", Content = "Content3", EditTime = DateTime.Now });
    }



}