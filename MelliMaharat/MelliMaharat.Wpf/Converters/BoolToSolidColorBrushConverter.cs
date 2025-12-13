namespace MelliMaharat.Wpf.Converters;

public class BoolToSolidColorBrushConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (new BrushConverter().ConvertFrom("#1B263B") is not SolidColorBrush brush) 
            throw new ArgumentNullException(nameof(brush));

        if (value is not bool hasError)
            return new SolidColorBrush(Transparent);
        else
            return hasError ? new SolidColorBrush(Red) : new BrushConverter().ConvertFrom("#1B263B");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}