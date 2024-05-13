using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;

namespace Notatnik_WPF;

public class SearchBarViewModel : BaseViewModel
{

    public string SearchText { get; set; } = string.Empty;
    public ICommand SearchCommand { get; set; }
    public ICommand ClearCommand { get; set; }



    void Search()
    {
        
    }
    void Clear()
    {
        SearchText = string.Empty;
        OnPropertyChanged(nameof(SearchText));
    }
}