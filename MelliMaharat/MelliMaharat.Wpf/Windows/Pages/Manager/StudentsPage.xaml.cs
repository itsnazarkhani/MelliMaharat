using MelliMaharat.Wpf.ViewModels.Pages.Manager;

namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for StudentsPage.xaml
/// </summary>
public partial class StudentsPage : Page
{
    public StudentsPage(ViewModels.Pages.Manager.StudentsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
