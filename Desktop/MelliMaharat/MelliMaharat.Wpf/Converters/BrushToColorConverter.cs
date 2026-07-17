namespace MelliMaharat.Wpf.Converters;

public class BrushToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is SolidColorBrush brush ? brush.Color : Black;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is Color c ? new SolidColorBrush(c) : new SolidColorBrush(Transparent);
}