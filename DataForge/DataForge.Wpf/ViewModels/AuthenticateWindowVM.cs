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
            }
        }
    }
}