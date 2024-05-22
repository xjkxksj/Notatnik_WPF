using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
    public void saveToFile(string fileName)
    {

    }
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