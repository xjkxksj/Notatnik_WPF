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
            File.Create(fileName).Close();
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

        if (Categories.Count == 0)
            Categories.Add(new Category() { Name = " " });
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
}