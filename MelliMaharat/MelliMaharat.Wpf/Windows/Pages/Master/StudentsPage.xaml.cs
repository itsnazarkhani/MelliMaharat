using MelliMaharat.Wpf.ViewModels.Pages;

namespace MelliMaharat.Wpf.Windows.Pages.Master;

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
