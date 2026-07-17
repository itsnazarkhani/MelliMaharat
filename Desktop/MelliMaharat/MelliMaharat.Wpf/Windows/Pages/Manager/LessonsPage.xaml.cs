namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for LessonsPage.xaml
/// </summary>
public partial class LessonsPage : Page
{
    public LessonsPage(LessonsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void Clear_Button_Click(object sender, RoutedEventArgs e) => listbox.UnselectAll();
}