using MelliMaharat.Wpf.ViewModels.Pages.Manager;

namespace MelliMaharat.Wpf.Windows.Pages.Student;

/// <summary>
/// Interaction logic for SelectionsPage.xaml
/// </summary>
public partial class SelectionsPage : Page
{
    public SelectionsPage(ViewModels.Pages.Student.SelectionsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
