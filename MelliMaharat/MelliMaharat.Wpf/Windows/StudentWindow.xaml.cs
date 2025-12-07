namespace MelliMaharat.Wpf.Windows;

/// <summary>
/// Interaction logic for StudentWindow.xaml
/// </summary>
public partial class StudentWindow : Window
{
    public StudentWindow(Student student)
    {
        InitializeComponent();
        DataContext = new StudentWindowVM(student, MainFrame);
    }
}
