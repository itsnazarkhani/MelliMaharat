using MelliMaharat.Wpf.ViewModels.Pages;
using MelliMaharat.Wpf.ViewModels.Windows;
using MelliMaharat.Wpf.Windows.Pages.Manager;

namespace MelliMaharat.Wpf.Windows;

/// <summary>
/// Interaction logic for StudentWindow.xaml
/// </summary>
public partial class StudentWindow : Window
{
    readonly StudentWindowVM _vm;
    readonly PresentationsPageVM _presentationsPageVM = new();
    readonly ProfilePageVM _profilePageVM = new();
    readonly SelectionsPageVM _selectionsPageVM = new();

    public StudentWindow(Student? student)
    {
        InitializeComponent();
        _vm = new();
        DataContext = _vm;
    }
}
