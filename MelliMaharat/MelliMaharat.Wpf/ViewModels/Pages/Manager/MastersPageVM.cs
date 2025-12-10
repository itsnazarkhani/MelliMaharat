namespace MelliMaharat.Wpf.ViewModels.Pages.Manager;

public class MastersPageVM : BaseVM
{
    #region Fields
    readonly MasterRepo _repo = new();
    #endregion
    #region Properties
    public ObservableCollection<Models.Master> Models { get; set; } = [];
    public Models.Master Model
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Birthdate));
            OnPropertyChanged(nameof(NationalCode));
            OnPropertyChanged(nameof(PhoneNumber));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(Graduation));
            OnPropertyChanged(nameof(Role));
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Department));
        }
    }
    public string FirstName
    {
        get => Model.User.PersonInformation.FirstName;
        set
        {
            Model.User.PersonInformation.FirstName = value;
            OnPropertyChanged();
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string LastName
    {
        get => Model.User.PersonInformation.LastName; 
        set
        {
            Model.User.PersonInformation.LastName = value;
            OnPropertyChanged();
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string Birthdate
    {
        get => Model.User.PersonInformation.BirthDate.ToString();
        set
        {
            Model.User.PersonInformation.BirthDate = DateOnly.Parse(value);
            OnPropertyChanged();
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string NationalCode 
    { 
        get => Model.User.PersonInformation.NationalCode; 
        set
        {
            Model.User.PersonInformation.NationalCode = value;
            OnPropertyChanged();
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string PhoneNumber 
    { 
        get => Model.User.PersonInformation.PhoneNumber; 
        set
        {
            Model.User.PersonInformation.PhoneNumber = value;
            OnPropertyChanged();
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string Email 
    {
        get => Model.User.Email;
        set
        {
            Model.User.Email = value;
            OnPropertyChanged();
            ValidateProperty(Model.User);
        }
    }
    public string Password 
    {
        get => Model.User.Password;
        set
        {
            Model.User.Password = value;
            OnPropertyChanged();
            ValidateProperty(Model.User);
        }
    }
    public bool IsAdmin 
    { 
        get => Model.User.Role == UserRoles.Admin;
        set
        {
            throw new ArgumentException("this Value Should not be set; its soppused to be readonly!");
        }
    }
    public string Username 
    { 
        get => Model.User.Username; 
        set
        {
            Model.User.Username = value;
            OnPropertyChanged();
            ValidateProperty(Model.User);
        }
    }
    public string Graduation 
    { 
        get => Model.Graduation.ToString(); 
        set
        {
            Model.Graduation = Enum.Parse<Graduations>(value);
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public string Role 
    { 
        get => Model.User.Role.ToString(); 
        set
        {
            Model.User.Role = Enum.Parse<UserRoles>(value);
            OnPropertyChanged();
            ValidateProperty(Model.User);
        }
    }
    public string Id 
    { 
        get => Model.Id.ToString(); 
        set => throw new ArgumentException("this Value Should not be set; its soppused to be readonly!");
    }
    public string Department
    { 
        get => Model.Department.Name.ToString();
        set
        {
            Model.Department.Name = Enum.Parse<Departments>(value);
            OnPropertyChanged();
            ValidateProperty(Model.Department);
        }
    }
    #endregion
    #region Constructors
    public MastersPageVM()
    {
        foreach(var m in _repo.GetAll())
            Models.Add(m);
        Model = Models.First();
    }
    #endregion
}
