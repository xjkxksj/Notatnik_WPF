using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik_WPF;
internal class Note
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime EditTime { get; set; }
    public List<Category> Categories { get; set; }
    public List<Tag> Tags { get; set; }
}