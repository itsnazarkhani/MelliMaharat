namespace MelliMaharat.Wpf.Converters;

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Color color = (Color)ColorConverter.ConvertFromString("#1B263B");

        if (value is bool result)
            return result ? Red : color;
        else
            return Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}