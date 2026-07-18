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
    private void Clear_Button_Click(object sender, RoutedEventArgs e) => listbox.UnselectAll();
}
