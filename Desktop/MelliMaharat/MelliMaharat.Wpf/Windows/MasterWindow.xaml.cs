namespace MelliMaharat.Wpf.Windows;

/// <summary>
/// Interaction logic for MasterWindow.xaml
/// </summary>
public partial class MasterWindow : Window
{
    public MasterWindow(Master master)
    {
        InitializeComponent();
        DataContext = new MasterWindowVM(master, MainFrame);
    }
}
