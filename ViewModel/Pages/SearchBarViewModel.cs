using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Notatnik_WPF;

public class SearchBarViewModel : BaseViewModel
{
    private string searchText;
    public string SearchText
    {
        get { return searchText; }
        set
        {
            searchText = value;
            Messenger.Send<string>("ChangeSearch", searchText);
        }
    }


}