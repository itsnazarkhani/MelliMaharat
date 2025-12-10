namespace MelliMaharat.Wpf.ViewModels.Pages;

public class StudentsPageVM : BaseVM
{
    public StudentsPageVM()
    {
        foreach(var item in _repo.GetAll())
            Models.Add(item);
        Model = Models.First();
    }

    readonly StudentRepo _repo = new();

    #region Properties
    public ObservableCollection<Student> Models { get; set; } = [];
    public Student Model
    {
        get => field; 
        set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(NationalCode));
            OnPropertyChanged(nameof(PhoneNumber));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(Role));
            OnPropertyChanged(nameof(Id));
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
    public string BirthDate 
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
            throw new ArgumentException("This Value Should Not Be Set!");
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
        set => throw new ArgumentException("Id Should Not Be Able To Set By User!"); 
    }
    #endregion
}
