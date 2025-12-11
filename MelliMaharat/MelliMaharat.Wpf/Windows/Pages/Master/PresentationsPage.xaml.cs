namespace MelliMaharat.Wpf.Windows.Pages.Master;

/// <summary>
/// Interaction logic for PresentationsPage.xaml
/// </summary>
public partial class PresentationsPage : Page
{
    public PresentationsPage(ViewModels.Pages.Master.PresentationsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
