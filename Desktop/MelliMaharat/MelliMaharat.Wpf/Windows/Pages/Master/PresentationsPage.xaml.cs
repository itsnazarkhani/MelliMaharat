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
    private void Clear_Button_Click(object sender, RoutedEventArgs e) => listbox.UnselectAll();
}
