namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for StudentsPage.xaml
/// </summary>
public partial class StudentsPage : Page
{
    readonly StudentRepo _repo;
    public StudentsPage(ViewModels.Pages.Manager.StudentsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
        _repo = new();
    }

    private void MyListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {   
        if (sender is ListBox l && l.SelectedItems is not null && l.SelectedItem is Models.Student s)
            GradeTextBox.InputText = _repo.GetAvgGrade(s).ToString(); 
    }
}
