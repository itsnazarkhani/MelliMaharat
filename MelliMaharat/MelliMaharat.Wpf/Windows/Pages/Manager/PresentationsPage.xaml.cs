using MelliMaharat.Wpf.ViewModels.Pages.Manager;

namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for PresentationsPage.xaml
/// </summary>
public partial class PresentationsPage : Page
{
    public PresentationsPage(ViewModels.Pages.Manager.PresentationsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
