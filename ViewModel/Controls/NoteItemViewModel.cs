
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


}
