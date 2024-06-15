using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik_WPF;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Note> Notes { get; set; } = new List<Note>();
    public List<Category> Categories { get; set; } = new List<Category>();
}
