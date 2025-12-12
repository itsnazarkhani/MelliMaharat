namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for MastersPage.xaml
/// </summary>
public partial class MastersPage : Page
{
    public MastersPage(MastersPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
    private void Clear_Button_Click(object sender, RoutedEventArgs e) => listbox.UnselectAll();
}