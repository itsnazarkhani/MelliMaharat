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
    private void Clear_Button_Click(object sender, RoutedEventArgs e) => listbox.UnselectAll();
}
