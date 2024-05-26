using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notatnik_WPF;
public partial class NoteItem : UserControl
{
    public NoteItem()
    {
        InitializeComponent();

        this.SizeChanged += NoteItem_SizeChanged;
    }

    private void NoteItem_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var viewModel = this.DataContext as NoteItemViewModel;

        viewModel?.UpdateVisibleTags(e.NewSize.Width);
    }
}