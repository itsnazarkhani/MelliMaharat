using MelliMaharat.Wpf.ViewModels.Pages;
using MelliMaharat.Wpf.ViewModels.Windows;
using MelliMaharat.Wpf.Windows.Pages.Manager;

namespace MelliMaharat.Wpf.Windows;

/// <summary>
/// Interaction logic for ManagerWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{
    readonly ManagerWindowVM _vm;
    readonly ProfilePageVM _profilePageVM;
    readonly MastersPageVM _mastersPageVM = new();
    readonly StudentsPageVM _studentsPageVM = new();
    readonly LessonsPageVM _lessonsPageVM = new();
    readonly PresentationsPageVM _presentationsPageVM = new();
    readonly SelectionsPageVM _selectionsPageVM = new();

    

    public ManagerWindow(User user)
    {
        InitializeComponent();
        _vm = new ManagerWindowVM(user);
        DataContext = _vm;
        _profilePageVM = new ProfilePageVM(user);

        MainFrame.Navigate(new ProfilePage(_profilePageVM));
    }


    /// <summary>
    /// All Of These Should Move To vm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProfileRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if(MainFrame is not null)
            MainFrame.Navigate(new ProfilePage(_profilePageVM));
    }

    private void MastersRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new MastersPage(_mastersPageVM));
    }

    private void StudentsRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new StudentsPage(_studentsPageVM));
    }

    private void LessonsRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new LessonsPage(_lessonsPageVM));
    }

    private void PresentationsRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new PresentationsPage(_presentationsPageVM));
    }

    private void SelectionsRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new SelectionsPage(_selectionsPageVM));
    }
}
