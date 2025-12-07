using MelliMaharat.Wpf.ViewModels.Pages;

namespace MelliMaharat.Wpf.Windows.Pages.Manager;

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
