namespace MelliMaharat.Wpf.ViewModels.Windows;

class MasterWindowVM : BaseVM
{
    readonly LessonsPageVM _lessonsPageVM = new();
    readonly PresentationsPageVM _presentationsPageVM = new();
    readonly ProfilePageVM _profilePageVM = new();
    readonly StudentsPageVM _studentsPageVM = new();
    readonly Master? _master;
    readonly Frame _frame;

    public CommandRelay ProfileCommand => new(() => _frame.Navigate(new ProfilePage(_profilePageVM)));
    public CommandRelay LessonsCommand => new(() => _frame.Navigate(new LessonsPage(_lessonsPageVM)));
    public CommandRelay PresentationsCommand => new(() => _frame.Navigate(new PresentationsPage(_presentationsPageVM)));
    public CommandRelay StudentsCommand => new(() => _frame.Navigate(new StudentsPage(_studentsPageVM)));

    public MasterWindowVM(Master master, Frame frame)
    {
        _master = master;
        _frame = frame;
        _frame.Navigate(new ProfilePage(_profilePageVM));
    }
}
