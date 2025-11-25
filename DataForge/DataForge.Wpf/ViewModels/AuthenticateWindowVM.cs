using System.DirectoryServices.ActiveDirectory;

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
    readonly Action SignIn = () => Show("This is Demo", "Sign In Completed!");

    private CommandRelay? signUpCommand;
    public CommandRelay SignUpCommand => signUpCommand ??= new CommandRelay(SignUp);
    readonly Action SignUp = () => Show("SignUp Completed");
}