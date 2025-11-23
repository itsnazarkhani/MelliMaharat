namespace DataForge.Wpf.Converters;

public class VisibilityToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return (visibility == Visibility.Visible) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green); 
        }
        return new SolidColorBrush(Colors.Gray);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
