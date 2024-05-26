using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik_WPF;
internal class FilterValues
{
    public Category Category { get; set; }
    public List<Tag> Tags { get; set; }
    public string SearchText { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

}
