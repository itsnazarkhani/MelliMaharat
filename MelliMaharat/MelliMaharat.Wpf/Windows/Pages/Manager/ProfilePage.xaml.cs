using MelliMaharat.Wpf.ViewModels.Pages;

namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for ProfilePage.xaml
/// </summary>
public partial class ProfilePage : Page
{
    public ProfilePage(User user)
    {
        InitializeComponent();
        DataContext = new ProfilePageVM(user);
    }
}
