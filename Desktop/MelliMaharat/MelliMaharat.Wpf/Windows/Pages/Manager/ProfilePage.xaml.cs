namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for ProfilePage.xaml
/// </summary>
public partial class ProfilePage : Page
{
    public ProfilePage(ViewModels.Pages.Manager.ProfilePageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
