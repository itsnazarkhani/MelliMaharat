using MelliMaharat.Wpf.ViewModels.Windows;
using MelliMaharat.Wpf.Windows.Pages.Manager;

namespace MelliMaharat.Wpf.Windows;

/// <summary>
/// Interaction logic for ManagerWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{
    readonly ManagerWindowVM _vm;
    public ManagerWindow(User user)
    {
        InitializeComponent();
        _vm = new ManagerWindowVM(user);
        DataContext = _vm;

        MainFrame.Navigate(new ProfilePage(user));
    }
}
