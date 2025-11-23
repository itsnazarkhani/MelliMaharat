namespace DataForge.Wpf.Converters
{
    public class BoolToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool hasError)
                return new SolidColorBrush(Colors.Transparent);
            return hasError ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.MediumAquamarine);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
