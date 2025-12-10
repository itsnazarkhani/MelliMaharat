namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for ProfilePage.xaml
/// </summary>
public partial class ProfilePage : Page
{
    public ProfilePage(ProfilePageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
