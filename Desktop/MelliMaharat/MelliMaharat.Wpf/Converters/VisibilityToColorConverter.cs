namespace MelliMaharat.Wpf.Converters;

public class VisibilityToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
            return visibility == Visible ? new SolidColorBrush(Red) : new SolidColorBrush(MediumAquamarine); // Change These Colors According to your Your Theme
        else
            return new SolidColorBrush(Gray);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}