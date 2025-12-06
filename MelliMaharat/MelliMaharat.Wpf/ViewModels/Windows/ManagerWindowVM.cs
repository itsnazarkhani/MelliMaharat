namespace MelliMaharat.Wpf.ViewModels.Windows;

class ManagerWindowVM : BaseVM<User>
{
    public ManagerWindowVM(User user)
    {
        Model = user;
    }


}