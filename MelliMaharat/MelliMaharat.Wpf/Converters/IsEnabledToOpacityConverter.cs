namespace MelliMaharat.Wpf.Converters;

class IsEnabledToOpacityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool a && value is not null)
            return a switch
            {
                true => 1.0,
                false => 0.5
            };
        return Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
