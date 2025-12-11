using MelliMaharat.Wpf.ViewModels.Pages.Manager;

namespace MelliMaharat.Wpf.Windows.Pages.Master;

/// <summary>
/// Interaction logic for StudentsPage.xaml
/// </summary>
public partial class StudentsPage : Page
{
    readonly StudentRepo _repo = new();
    public StudentsPage(ViewModels.Pages.Master.StudentsPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void MyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListBox l && l.SelectedItem is not null && l.SelectedItem is Models.Student s)
            GradeTextBox.InputText = _repo.GetAvgGrade(s).ToString();
    }
}
