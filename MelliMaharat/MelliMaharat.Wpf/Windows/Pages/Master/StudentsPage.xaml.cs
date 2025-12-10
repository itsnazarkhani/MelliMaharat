using MelliMaharat.Wpf.ViewModels.Pages.Manager;

namespace MelliMaharat.Wpf.Windows.Pages.Master;

/// <summary>
/// Interaction logic for StudentsPage.xaml
/// </summary>
public partial class StudentsPage : Page
{
    public StudentsPage(ViewModels.Pages.Master.StudentsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
