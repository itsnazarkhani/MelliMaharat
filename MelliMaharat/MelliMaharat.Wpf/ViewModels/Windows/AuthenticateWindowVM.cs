using MelliMaharat.Dal.Repos.Base;
using MelliMaharat.Dal.UnitOfWork.MelliMaharat.Dal.UnitOfWork;

namespace MelliMaharat.Wpf.ViewModels.Windows;

public class AuthenticateWindowVM : BaseVM<User>
{
    #region Constructor
    public AuthenticateWindowVM() : base() { }
    
    #endregion

    #region Properties
    public string Username
    {
        get => Model.Username;
        set
        {
            if (Model.Username != value)
            {
                Model.Username = value;
                OnPropertyChanged();
                ValidateProperty(Model);
                SignInCommand.NotifyCanExecuteChanged();
            }
        }
    }
    public string Password
    {
        get => Model.Password;
        set
        {
            if (Model.Password != value)
            {
                Model.Password = value;
                OnPropertyChanged();
                ValidateProperty(Model);
                SignInCommand.NotifyCanExecuteChanged();
            }
        }
    }
    #endregion

    #region Commands
    private CommandRelay<Window>? signInCommand = null;
    public CommandRelay<Window> SignInCommand => signInCommand ??= new CommandRelay<Window>(SignIn, CanSignIn);
    bool CanSignIn(Window? parameter) => !HasErrors && !IsNullOrEmpty(Password) && !IsNullOrEmpty(Username);
    async void SignIn(Window parameter)
    {
        CurrentUserRole = UserRoles.None;
        CurrentUser = new();

        //var unitOfWork = new UnitOfWork(new ApplicationDbContextFactory().CreateDbContext());
        //var authService = new AuthService(unitOfWork);
        var context = new ApplicationDbContextFactory().CreateDbContext();
        var authService = new AuthService(context);
        var studentRepo = new StudentRepo();
        var masterRepo = new MasterRepo();
        var managerRepo = new UserRepo();

        //AuthResult authResult = authResultTask.GetAwaiter().GetResult();
        var authResult = await authService.LoginAsync(Username, Password);
        
        // if user credentials wrong
        if (!authResult.IsSuccess)
        {
            Show(authResult.Message);
            return;
        }

        CurrentUserRole = authResult.User!.Role;
        try
        {

            switch (CurrentUserRole)
            {
                case UserRoles.Student:
                    //CurrentUser = unitOfWork.Students.GetAll().Where(x => x.User.Username == Username).First();
                    CurrentUser = studentRepo.GetSingle(Username);
                    new StudentWindow((Student)CurrentUser).Show();
                    break;
                case UserRoles.Master:
                    //CurrentUser = unitOfWork.Masters.GetAll().Where(x => x.User.Username == Username).First();
                    CurrentUser = masterRepo.GetSingle(Username);
                    new MasterWindow((Master)CurrentUser).Show();
                    break;
                case UserRoles.Admin:
                    //CurrentUser = unitOfWork.Users.GetAll().Where(x => x.Username == Username).First();
                    CurrentUser = managerRepo.GetSingle(Username);
                    new ManagerWindow((User)CurrentUser).Show();
                    break;
                default:
                    Show("This User Role Is Undefined!");
                    return;
            }
        }
        catch (Exception ex)
        {
            Show(ex.Message, "An Error Occured During Authentication!");
            return;
        }
        parameter.Close();
    }
    #endregion
}