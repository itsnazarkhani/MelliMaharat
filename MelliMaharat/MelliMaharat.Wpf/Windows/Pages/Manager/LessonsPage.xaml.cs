using MelliMaharat.Wpf.ViewModels.Pages.Manager;

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
}