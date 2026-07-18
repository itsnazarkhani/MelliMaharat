 namespace MelliMaharat.Wpf.Windows;

/// <summary>
/// Interaction logic for AuthenticateWindow.xaml
/// </summary>
public partial class AuthenticateWindow : Window
{
    public AuthenticateWindow() => InitializeComponent();

    private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();
    private void Button_Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    private void Button_Exit_Click(object sender, RoutedEventArgs e) => Close();
}