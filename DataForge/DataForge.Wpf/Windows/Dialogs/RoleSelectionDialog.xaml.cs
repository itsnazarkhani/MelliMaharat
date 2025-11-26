namespace DataForge.Wpf.Windows.Dialogs;

/// <summary>
/// Interaction logic for RoleSelectionDialog.xaml
/// </summary>
public partial class RoleSelectionDialog : Window
{
    public RoleSelectionDialog() => InitializeComponent();

    private void Cancel_Hyperlink_Click(object sender, RoutedEventArgs e) => Close();

    private void btnStudent_Click(object sender, RoutedEventArgs e)
    {
        new StudentDialog().ShowDialog();
        Close();
    }

    private void btnMaster_Click(object sender, RoutedEventArgs e)
    {
        new MasterDialog().ShowDialog();
        Close();
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();
}
