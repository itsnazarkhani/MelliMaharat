namespace MelliMaharat.Wpf.Windows;

/// <summary>
/// Interaction logic for ManagerWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{
    public ManagerWindow(User user)
    {
        InitializeComponent();
        DataContext = new ManagerWindowVM(user, MainFrame);
    }
}
