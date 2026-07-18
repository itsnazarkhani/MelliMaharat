namespace MelliMaharat.Wpf.Windows.Pages.Student;

/// <summary>
/// Interaction logic for ProfilePage.xaml
/// </summary>
public partial class ProfilePage : Page
{
    public ProfilePage(ViewModels.Pages.Student.ProfilePageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
