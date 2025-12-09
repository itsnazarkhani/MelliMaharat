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

        var unitOfWork = new UnitOfWork(new ApplicationDbContextFactory().CreateDbContext());
        var authService = new AuthService(unitOfWork);


        //AuthResult authResult = authResultTask.GetAwaiter().GetResult();
        var authResult = await authService.LoginAsync(Username, Password);
        
        // if user credentials wrong
        if (!authResult.IsSuccess)
        {
            Show(authResult.Message);
            return;
        }

        // current user 
        //_ = authResult.User;

        // current user role
        //_ = authResult.User!.Role;

        CurrentUserRole = authResult.User!.Role;

        switch (CurrentUserRole)
        {
            case UserRoles.Student:
                CurrentUser = unitOfWork.Students.GetAll().Where(x => x.User.Username == Username).First();
                new StudentWindow((Student)CurrentUser).Show();
                break;
            case UserRoles.Master:
                CurrentUser = unitOfWork.Masters.GetAll().Where(x => x.User.Username == Username).First();
                new MasterWindow((Master)CurrentUser).Show();
                break;
            case UserRoles.Admin:
                CurrentUser = unitOfWork.Users.GetAll().Where(x => x.Username == Username).First();
                new ManagerWindow((User)CurrentUser).Show();
                break;
            default:
                Show("This User Role Is Undefined!");
                return;
        }
        parameter.Close();
    }
    #endregion
}