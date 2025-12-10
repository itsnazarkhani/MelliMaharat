namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for StudentsPage.xaml
/// </summary>
public partial class StudentsPage : Page
{
    public StudentsPage(StudentsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
