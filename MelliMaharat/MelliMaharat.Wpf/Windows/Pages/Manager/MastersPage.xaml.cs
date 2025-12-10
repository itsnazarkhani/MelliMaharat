using MelliMaharat.Wpf.ViewModels.Pages.Manager;

namespace MelliMaharat.Wpf.Windows.Pages.Manager;

/// <summary>
/// Interaction logic for MastersPage.xaml
/// </summary>
public partial class MastersPage : Page
{
    public MastersPage(MastersPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
        listbox.SelectedIndex = 0;
    }
}