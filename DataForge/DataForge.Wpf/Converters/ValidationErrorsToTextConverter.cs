namespace DataForge.Wpf.Converters;

public class ValidationErrorsToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // WPF can pass null, UnsetValue, or wrong types
        if (value == null || value == DependencyProperty.UnsetValue)
            return "";

        if (value is not ReadOnlyObservableCollection<ValidationError> errors)
            return "";

        var messages = errors
            .Select(e => e.ErrorContent?.ToString())
            .Where(msg => !string.IsNullOrWhiteSpace(msg));

        return string.Join("\n* ", messages);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}