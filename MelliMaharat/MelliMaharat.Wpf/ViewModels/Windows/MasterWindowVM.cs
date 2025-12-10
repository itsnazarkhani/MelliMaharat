namespace MelliMaharat.Wpf.ViewModels.Windows;

class MasterWindowVM : BaseVM
{
    readonly LessonPageVM _lessonsPageVM;
    readonly Pages.Master.PresentationsPageVM _presentationsPageVM;
    readonly Pages.Master.ProfilePageVM _profilePageVM;
    readonly Pages.Master.StudentsPageVM _studentsPageVM;
    readonly Master _master;
    readonly Frame _frame;

    public CommandRelay ProfileCommand => new(() => _frame.Navigate(new Wpf.Windows.Pages.Master.ProfilePage(_profilePageVM)));
    public CommandRelay LessonsCommand => new(() => _frame.Navigate(new Wpf.Windows.Pages.Master.LessonsPage(_lessonsPageVM)));
    public CommandRelay PresentationsCommand => new(() => _frame.Navigate(new Wpf.Windows.Pages.Master.PresentationsPage(_presentationsPageVM)));
    public CommandRelay StudentsCommand => new(() => _frame.Navigate(new Wpf.Windows.Pages.Master.StudentsPage(_studentsPageVM)));

    public MasterWindowVM(Master master, Frame frame)
    {
        _master = master;
        _frame = frame;
        _lessonsPageVM = new LessonPageVM(master);
        _presentationsPageVM = new(master);
        _studentsPageVM = new(master);
        _profilePageVM = new(master);

        _frame.Navigate(new Wpf.Windows.Pages.Master.ProfilePage(_profilePageVM));
    }
}
