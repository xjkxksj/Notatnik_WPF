using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Notatnik_WPF;

public class IndexToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var item = value as ComboBoxItem;
        if (item != null)
        {
            var comboBox = ItemsControl.ItemsControlFromItemContainer(item) as ComboBox;
            if (comboBox != null)
            {
                int index = comboBox.ItemContainerGenerator.IndexFromContainer(item);
                return index == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}