namespace DataForge.Wpf.Resources.UserControls;

/// <summary>
/// Interaction logic for CustomTextBox.xaml
/// </summary>
public partial class CustomTextBox : UserControl
{
    public static readonly DependencyProperty InputTextProperty;
    public string InputText
    {
        get { return (string)GetValue(InputTextProperty); }
        set { SetValue(InputTextProperty, value); }
    }


    public CustomTextBox() =>InitializeComponent();
    static CustomTextBox() =>
        InputTextProperty = DependencyProperty.Register(nameof(InputText), typeof(string), typeof(CustomTextBox), new FrameworkPropertyMetadata(defaultValue: "", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    private void txtInput_TextChanged(object sender, TextChangedEventArgs e) =>
        lblError.Visibility = string.IsNullOrWhiteSpace(lblError.Text) ? Visibility.Hidden : Visibility.Visible;
}
