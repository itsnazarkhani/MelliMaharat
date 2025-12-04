using MelliMaharat.Dal.DbContexts;
using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Dal.UnitOfWork.MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Infrastructure.Services;
using MelliMaharat.Models.Enums;

namespace MelliMaharat.Wpf.ViewModels;

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
    void SignIn(Window parameter)
    {
        const string roleKey = "user_role";
        Application.Current.Properties[roleKey] = null;

        //var masterRepo = new MasterRepo();
        //var studentRepo = new StudentRepo();
        //bool isStudent = studentRepo.IsUserExist(Username);
        //bool isMaster = masterRepo.IsUserExist(Username);

        IUnitOfWork unitOfWork = new UnitOfWork(new ApplicationDbContext());
        AuthService authService = new AuthService(unitOfWork);


        var authResult = authService.LoginAsync(Username, Password).GetAwaiter().GetResult();

        // if user credentials wrong
        if (!authResult.IsSuccess)
        {
            Show(authResult.Message);
            return;
        }

        if (isStudent && studentRepo.IsPasswordMatch(Username, Password))
        {
            Application.Current.Properties[roleKey] = "student";
            Student currentUser = new StudentRepo().GetAll().Where(x => x.PersonInformation.Username == Username).Single();
            Application.Current.Properties["current_user"] = currentUser;
        }
        else if (isMaster && masterRepo.IsPasswordMatch(Username, Password))
        {
            Application.Current.Properties[roleKey] = "master";
            Master currentUser = new MasterRepo().GetAll().Where(x => x.PersonInformation.Username == Username).Single();
            Application.Current.Properties["current_user"] = currentUser;
        }
        //else if (isManager && managerRepo.IsPasswordMatch(Username, Password))
        //{
        //    Application.Current.Properties[roleKey] = "manager";
        //    Manager currentUser = new ManagerRepo().GetAll().Where(x => x.PersonInformation.Username == Username).Single();
        //    Application.Current.Properties["current_user"] = currentUser;
        //}

        if (Application.Current.Properties[roleKey] is string role && (role is "student" or "master" or "manager"))
        {
            case UserRoles.Student:
                Application.Current.Properties[roleKey] = "student";
                break;
            case UserRoles.Master:
                Application.Current.Properties[roleKey] = "master";
                break;
        }
        else
            Show("Password doesn't match.", "Try again!");
    }

    private CommandRelay? signUpCommand;
    public CommandRelay SignUpCommand => signUpCommand ??= new CommandRelay(SignUp);
    readonly Action SignUp = () => new RoleSelectionDialog().ShowDialog();
    #endregion
}