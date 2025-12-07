using MelliMaharat.Wpf.ViewModels.Pages;
using MelliMaharat.Wpf.ViewModels.Windows;

namespace MelliMaharat.Wpf.Windows;

/// <summary>
/// Interaction logic for MasterWindow.xaml
/// </summary>
public partial class MasterWindow : Window
{
    readonly MasterWindowVM _vm;
    readonly LessonsPageVM _lessonsPageVM = new();
    readonly PresentationsPageVM _presentationsPageVM = new();
    readonly ProfilePageVM _profilePageVM = new();
    readonly StudentsPageVM _studentsPageVM = new();

    public MasterWindow(Master? master)
    {
        InitializeComponent();
        _vm = new MasterWindowVM();
        DataContext = _vm;
    }
}
