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

    public CustomTextBox() => InitializeComponent();

    static CustomTextBox() =>
        InputTextProperty = DependencyProperty.Register(nameof(InputText), typeof(string), typeof(CustomTextBox), new FrameworkPropertyMetadata(defaultValue: "", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    private void Root_GotFocus(object sender, RoutedEventArgs e) =>
        SetTargetForStoryBoard("lblTagFadeOut", lblTag);

    private void Root_LostFocus(object sender, RoutedEventArgs e) =>
        _ = string.IsNullOrEmpty(txtInput.Text) && SetTargetForStoryBoard("lblTagFadeIn", lblTag);

    
    bool SetTargetForStoryBoard(string storyboardName, DependencyObject value)
    {
        Storyboard storyboard = (Storyboard)FindResource(storyboardName);
        Storyboard clone = storyboard.Clone();

        foreach (var item in clone.Children)
            Storyboard.SetTarget(item, value);

        clone.Begin();
        return true;
    }

    private void mainBorder_MouseDown(object sender, MouseButtonEventArgs e) => txtInput.Focus();
}
