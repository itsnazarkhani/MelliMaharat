namespace MelliMaharat.Wpf.ViewModels.Windows;

class ManagerWindowVM : BaseVM<User>
{
    #region Fields
    readonly Pages.Manager.ProfilePageVM _profilePageVM;
    readonly MastersPageVM _mastersPageVM = new();
    readonly Pages.Manager.StudentsPageVM _studentsPageVM = new();
    readonly LessonsPageVM _lessonsPageVM = new();
    readonly Pages.Manager.PresentationsPageVM _presentationsPageVM = new();
    readonly SelectionsPageVM _selectionsPageVM = new();
    readonly Frame _frame;
    #endregion
    #region Commands
    public CommandRelay ProfileCommand => new(() => _frame.Navigate(new ProfilePage(_profilePageVM)));
    public CommandRelay MastersCommand => new(() => _frame.Navigate(new MastersPage(_mastersPageVM)));
    public CommandRelay StudentsCommand => new(() => _frame.Navigate(new StudentsPage(_studentsPageVM)));
    public CommandRelay LessonsCommand => new(() => _frame.Navigate(new LessonsPage(_lessonsPageVM)));
    public CommandRelay PresentationsCommand => new(() => _frame.Navigate(new PresentationsPage(_presentationsPageVM)));
    public CommandRelay SelectionsCommand => new(() => _frame.Navigate(new SelectionsPage(_selectionsPageVM)));
    #endregion
    #region Constructor
    public ManagerWindowVM(User user, Frame frame)
    {
        Model = user;
        _profilePageVM = new Pages.Manager.ProfilePageVM(user);
        _frame = frame;

        _frame.Navigate(new ProfilePage(_profilePageVM));
    }
    #endregion
}