namespace MelliMaharat.Wpf.ViewModels.Windows;

class StudentWindowVM : BaseVM
{
    #region Fields
    readonly Frame _frame;
    readonly Pages.Student.PresentationsPageVM _presentationsPageVM;
    readonly Pages.Student.ProfilePageVM _profilePageVM;
    readonly Pages.Student.SelectionsPageVM _selectionsPageVM;
    #endregion
    #region Commands
    public CommandRelay ProfileCommand => new(() => _frame.Navigate(new Wpf.Windows.Pages.Student.ProfilePage(_profilePageVM)));
    public CommandRelay PresentationsCommand => new(() => _frame.Navigate(new Wpf.Windows.Pages.Student.PresentationsPage(_presentationsPageVM)));
    public CommandRelay SelectionsCommand => new(() => _frame.Navigate(new Wpf.Windows.Pages.Student.SelectionsPage(_selectionsPageVM)));
    #endregion
    #region Constructor
    public StudentWindowVM(Student student, Frame frame)
    {
        _frame = frame;
        _presentationsPageVM = new(student);
        _profilePageVM = new(student);
        _selectionsPageVM = new(student);

        _frame.Navigate(new Wpf.Windows.Pages.Student.ProfilePage(_profilePageVM));
    }
    #endregion
}
