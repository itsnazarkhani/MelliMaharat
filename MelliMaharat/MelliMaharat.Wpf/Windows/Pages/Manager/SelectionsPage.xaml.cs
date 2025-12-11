namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for SelectionsPage.xaml
/// </summary>
public partial class SelectionsPage : Page
{
    public SelectionsPage(SelectionsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}