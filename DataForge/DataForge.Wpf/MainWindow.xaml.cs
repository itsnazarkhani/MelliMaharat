using DataForge.Wpf.ViewModels;

namespace DataForge.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindowViewModel _dataContext { get; set; } = new MainWindowViewModel();
    public MainWindow() => InitializeComponent();
}