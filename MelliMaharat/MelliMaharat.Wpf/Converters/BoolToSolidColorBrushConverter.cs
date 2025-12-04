namespace MelliMaharat.Wpf.Converters;

public class BoolToSolidColorBrushConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        SolidColorBrush? brush = new BrushConverter().ConvertFrom("#1B263B") as SolidColorBrush;
        if (brush == null ) throw new ArgumentNullException(nameof(brush));

        if (value is not bool hasError)
            return new SolidColorBrush(Transparent);
        else
            return hasError ? new SolidColorBrush(Red) : new BrushConverter().ConvertFrom("#1B263B");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}