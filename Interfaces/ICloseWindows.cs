using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notatnik_WPF
{
    interface ICloseWindows
    {
        Action Close { get; set; }
        bool CanClose();
    }
}
