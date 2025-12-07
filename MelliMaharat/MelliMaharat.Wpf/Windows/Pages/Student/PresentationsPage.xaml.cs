using MelliMaharat.Wpf.ViewModels.Pages;

namespace MelliMaharat.Wpf.Windows.Pages.Student;

/// <summary>
/// Interaction logic for PresentationsPage.xaml
/// </summary>
public partial class PresentationsPage : Page
{
    public PresentationsPage(PresentationsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
