namespace MelliMaharat.Wpf.ViewModels.Windows;

class StudentWindowVM : BaseVM
{
    readonly Student _student;
    readonly Frame _frame;
    readonly PresentationsPageVM _presentationsPageVM = new();
    readonly ProfilePageVM _profilePageVM = new();
    readonly SelectionsPageVM _selectionsPageVM = new();

    public CommandRelay ProfileCommand => new(() => _frame.Navigate(new ProfilePage(_profilePageVM)));
    public CommandRelay PresentationsCommand => new(() => _frame.Navigate(new PresentationsPage(_presentationsPageVM)));
    public CommandRelay SelectionsCommand => new(() => _frame.Navigate(new SelectionsPage(_selectionsPageVM)));

    public StudentWindowVM(Student student, Frame frame)
    {
        _student = student;
        _frame = frame;
    }
}
