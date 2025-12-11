namespace MelliMaharat.Wpf.Windows.Pages.Student;

/// <summary>
/// Interaction logic for PresentationsPage.xaml
/// </summary>
public partial class PresentationsPage : Page
{
    public PresentationsPage(ViewModels.Pages.Student.PresentationsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
