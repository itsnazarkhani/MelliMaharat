namespace MelliMaharat.Wpf.Converters
{
    public class BoolToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool hasError)
                return new SolidColorBrush(Transparent);
            else
                return hasError ? new SolidColorBrush(Red) : new SolidColorBrush(MediumAquamarine);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
