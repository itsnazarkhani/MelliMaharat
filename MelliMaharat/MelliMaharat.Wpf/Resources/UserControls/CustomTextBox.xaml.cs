namespace MelliMaharat.Wpf.Resources.UserControls;
/// <summary>
/// Interaction logic for CustomTextBox.xaml
/// </summary>
public partial class CustomTextBox : UserControl
{
    #region fields
    public static readonly DependencyProperty InputTextProperty;
    public static readonly DependencyProperty HintProperty;
    #endregion

    #region properties
    public string InputText
    {
        get { return (string)GetValue(InputTextProperty); }
        set { SetValue(InputTextProperty, value); }
    }
    public string Hint
    {
        get { return (string)GetValue(HintProperty); }
        set { SetValue(HintProperty, value); }
    }
    #endregion

    #region constructors
    public CustomTextBox() => InitializeComponent();

    static CustomTextBox()
    {
        InputTextProperty = DependencyProperty.Register(nameof(InputText), typeof(string), typeof(CustomTextBox), new FrameworkPropertyMetadata("", BindsTwoWayByDefault));
        HintProperty = DependencyProperty.Register(nameof(Hint), typeof(string), typeof(CustomTextBox), new PropertyMetadata(""));
    }
    #endregion

    #region events
    private void Root_GotFocus(object sender, RoutedEventArgs e) =>
        SetTargetForStoryBoard("lblTagFadeOut", lblHint);
    private void Root_LostFocus(object sender, RoutedEventArgs e) =>
        _ = IsNullOrEmpty(txtInput.Text) && SetTargetForStoryBoard("lblTagFadeIn", lblHint);
    private void mainBorder_MouseDown(object sender, MouseButtonEventArgs e) => txtInput.Focus();
    #endregion

    #region helper methods
    bool SetTargetForStoryBoard(string storyboardName, DependencyObject value)
    {
        Storyboard storyboard = (Storyboard)FindResource(storyboardName);
        Storyboard clone = storyboard.Clone();

        foreach (var item in clone.Children)
            SetTarget(item, value);

        clone.Begin();
        return true;
    }
    #endregion
}