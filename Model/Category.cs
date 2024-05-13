using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik_WPF;
internal class Category
{
    public string Name { get; set; }
    public List<Note> Notes { get; set; }
}