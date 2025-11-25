namespace DataForge.Wpf.ViewModels;

public class AuthenticateWindowVM : BaseVM<Person>
{
    public AuthenticateWindowVM() : base() { }

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

    private CommandRelay? signInCommand = null;
    public CommandRelay SignInCommand => signInCommand ??= new CommandRelay(SignIn, CanSignIn);
    bool CanSignIn() => !HasErrors && !IsNullOrEmpty(Password) && !IsNullOrEmpty(Username);
    void SignIn()
    {
        const string roleKey = "user_role";
        Application.Current.Properties[roleKey] = null;

        var masterRepo = new MasterRepo();
        var studentRepo = new StudentRepo();

        bool isStudent = studentRepo.IsUserExist(Username);
        bool isMaster = masterRepo.IsUserExist(Username);

        if (!isStudent && !isMaster)
        {
            Show("No account exists with this username.");
            return;
        }

        if (isStudent && studentRepo.IsPasswordMatch(Username, Password))
            Application.Current.Properties[roleKey] = "student";
        else if (isMaster && masterRepo.IsPasswordMatch(Username, Password))
            Application.Current.Properties[roleKey] = "master";

        if (Application.Current.Properties[roleKey] is string role && (role is "student" or "master"))
            Show("This is Demo", $"Sign-In completed as {role}!");        
        else
            Show("Password doesn't match.", "Try again!");
    }



    private CommandRelay? signUpCommand;
    public CommandRelay SignUpCommand => signUpCommand ??= new CommandRelay(SignUp);
    readonly Action SignUp = () => Show("SignUp Completed");
}