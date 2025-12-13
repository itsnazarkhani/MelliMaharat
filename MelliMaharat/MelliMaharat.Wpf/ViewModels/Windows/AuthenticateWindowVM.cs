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
                ValidateProperty(Model);
                SignInCommand.NotifyCanExecuteChanged();
            }
        }
    }
    #endregion
    #region Commands
    public CommandRelay<Window> SignInCommand => field ??= new CommandRelay<Window>(SignIn, CanSignIn);
    bool CanSignIn(Window? parameter) => !HasErrors && !IsNullOrEmpty(Password) && !IsNullOrEmpty(Username);
    async void SignIn(Window parameter)
    {
        var authService = new AuthService(new ApplicationDbContextFactory().CreateDbContext());
        var authResult = await authService.LoginAsync(Username, Password);
        
        // if user credentials wrong
        if (!authResult.IsSuccess)
        {
            Show(authResult.Message);
            return;
        }
        if (authResult.User is null)
        {
            Show("User Role Is Not Defined!");
            return;
        }


        switch (authResult.User!.Role)
        {
            case UserRoles.None:
                Show("This User Role Is Undefined!");
                break;
            case UserRoles.Admin:
                new ManagerWindow(authResult.User).Show();
                break;
            case UserRoles.Student:
                new StudentWindow(authResult.User.Student).Show();
                break;
            case UserRoles.Master:
                new MasterWindow(authResult.User.Master).Show();
                break;
        }
        parameter.Close();
    }
    #endregion
}