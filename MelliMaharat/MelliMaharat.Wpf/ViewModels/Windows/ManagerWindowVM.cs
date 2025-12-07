namespace MelliMaharat.Wpf.ViewModels.Windows;

class ManagerWindowVM : BaseVM<User>
{
    readonly ProfilePageVM _profilePageVM;
    readonly MastersPageVM _mastersPageVM = new();
    readonly StudentsPageVM _studentsPageVM = new();
    readonly LessonsPageVM _lessonsPageVM = new();
    readonly PresentationsPageVM _presentationsPageVM = new();
    readonly SelectionsPageVM _selectionsPageVM = new();
    readonly Frame _frame;

    public CommandRelay ProfileCommand => new(() => _frame.Navigate(new ProfilePage(_profilePageVM)));
    public CommandRelay MastersCommand => new(() => _frame.Navigate(new MastersPage(_mastersPageVM)));
    public CommandRelay StudentsCommand => new(() => _frame.Navigate(new StudentsPage(_studentsPageVM)));
    public CommandRelay LessonsCommand => new(() => _frame.Navigate(new LessonsPage(_lessonsPageVM)));
    public CommandRelay PresentationsCommand => new(() => _frame.Navigate(new PresentationsPage(_presentationsPageVM)));
    public CommandRelay SelectionsCommand => new(() => _frame.Navigate(new SelectionsPage(_selectionsPageVM)));

    public ManagerWindowVM(User user, Frame frame)
    {
        Model = user;
        _profilePageVM = new ProfilePageVM(user);
        _frame = frame;

        _frame.Navigate(new ProfilePage(_profilePageVM));
    }
}