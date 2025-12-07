using MelliMaharat.Wpf.ViewModels.Pages;

namespace MelliMaharat.Wpf.ViewModels.Windows;

class ManagerWindowVM : BaseVM<User>
{
    readonly ProfilePageVM _profilePageVM;
    readonly MastersPageVM _mastersPageVM = new();
    readonly StudentsPageVM _studentsPageVM = new();
    readonly LessonsPageVM _lessonsPageVM = new();
    readonly PresentationsPageVM _presentationsPageVM = new();
    readonly SelectionsPageVM _selectionsPageVM = new();
    public Frame _frame;

    public ManagerWindowVM(User user, Frame frame)
    {
        Model = user;
        _profilePageVM = new ProfilePageVM(user);
        _frame = frame;
    }

    //CommandRelay ProfileRadioButton = new CommandRelay(execute: () => _frame.Navigate(new ProfilePage(_profilePageVM)));
    //CommandRelay MastersRadioButton = new CommandRelay();
    //CommandRelay StudentsRadioButton = new CommandRelay();
    //CommandRelay LessonsRadioButton = new CommandRelay();
    //CommandRelay PresentationsRadioButton = new CommandRelay();
    //CommandRelay SelectionsRadioButton = new CommandRelay();
}