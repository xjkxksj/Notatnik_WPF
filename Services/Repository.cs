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
    public List<Note> Notes { get => User.Notes; set => User.Notes = value; }
    public List<Category> Categories { get => User.Categories; set => User.Categories = value; }
    public List<User> Users { get; set; }  = new List<User>();
    public User? User { get; internal set; }


    public void loadFromFile(string fileName)
    {
        if (Users.Count > 0)
            return;

        //int emptyLinesCount = 0;
        if (!File.Exists(fileName))
            File.Create(fileName).Close();
        foreach (string line in File.ReadLines(fileName))
        {
            if (line.Length > 1)
            {
                Users.Add(JsonSerializer.Deserialize<User>(line));

                //if (line.Contains("Title"))
                //    Notes.Add(JsonSerializer.Deserialize<Note>(line));
                //else if (line.Contains("Name") && emptyLinesCount == 1)
                //    Categories.Add(JsonSerializer.Deserialize<Category>(line));
                //else if (line.Contains("Name") && emptyLinesCount == 2)
                //    Tags.Add(JsonSerializer.Deserialize<Tag>(line));
                //else if (line.Contains("User") && emptyLinesCount == 3)
                //    Users.Add(JsonSerializer.Deserialize<User>(line));
            }
            //else { emptyLinesCount++; }
        }

        if (Users.Count == 0)
            Users.Add(new User() { Username = "", Password="" });

        if (User == null)
        {
            User = Users[0];
        }

        if (Categories.Count == 0)
            Categories.Add(new Category() { Name = " " });
    }
    public void saveToFile(string fileName)
    {
        foreach (var user in Users)
        {
            if(user.Categories.Count == 0)
                user.Categories.Add(new Category() { Name = " " });
        }
        File.WriteAllText(fileName, string.Empty);
        //foreach (var note in Notes)
        //{
        //    File.AppendAllLines(fileName, [JsonSerializer.Serialize(note)]);
        //}
        //File.AppendAllText(fileName, "\n");
        //foreach (var category in Categories)
        //{
        //    File.AppendAllLines(fileName, [JsonSerializer.Serialize(category)]);
        //}
        //File.AppendAllText(fileName, "\n");
        //foreach (var tag in Tags)
        //{
        //    File.AppendAllLines(fileName, [JsonSerializer.Serialize(tag)]);
        //}
        //File.AppendAllText(fileName, "\n");
        foreach (var user in Users)
        {
            File.AppendAllLines(fileName, [JsonSerializer.Serialize(user)]);
        }
    }



}