using MelliMaharat.Wpf.ViewModels.Pages;

namespace MelliMaharat.Wpf.Windows.Pages.Master;

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
