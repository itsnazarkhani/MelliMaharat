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
            field ??= EmptyMaster;
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
        get => Model.User.PersonInformation.BirthDate == default ? Empty : Model.User.PersonInformation.BirthDate.ToString();
        set
        {
            try
            {
                Model.User.PersonInformation.BirthDate = DateOnly.Parse(value);
            }
            catch (Exception ex)
            {
                OnPropertyChanged();
                AddError(ex.Message);
                Model.User.PersonInformation.BirthDate = default;
                return;
            }
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
    public string IsAdmin 
    { 
        get => Model.User.Role == UserRoles.None ? Empty : (Model.User.Role == UserRoles.Admin).ToString();
        set
        {
            Show("this Value Should not be set; its soppused to be readonly!");
            OnPropertyChanged();
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
        get => Model.Graduation == Graduations.None ? Empty : Model.Graduation.ToString(); 
        set
        {
            try
            {
                Model.Graduation = Enum.Parse<Graduations>(value);
            }
            catch (Exception ex)
            {
                Model.Graduation = Graduations.None;
                AddError(ex.Message);
                OnPropertyChanged();
                return;
            }

            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public string Role 
    { 
        get => Model.User.Role == UserRoles.None ? Empty : Model.User.Role.ToString(); 
        set
        {
            try
            {
                Model.User.Role = Enum.Parse<UserRoles>(value);
            }
            catch (Exception ex)
            {
                OnPropertyChanged();
                AddError(ex.Message);
                return;
            }
            OnPropertyChanged();
            ValidateProperty(Model.User);
        }
    }
    public string Id
    {
        get => Model.Id == default ? Empty : Model.Id.ToString();
        set 
        {
            Show("this Value Should not be set; its soppused to be readonly!");
            OnPropertyChanged();
        } 
    }
    public string Department
    { 
        get => Model.Department.Name == Departments.None ? Empty : Model.Department.Name.ToString();
        set
        {
            try
            {
                Model.Department.Name = Enum.Parse<Departments>(value);
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
                OnPropertyChanged();
                Model.Department.Name = Departments.None;
                return;
            }
            OnPropertyChanged();
            ValidateProperty(Model.Department);
        }
    }
    #endregion
    #region Constructors
    public MastersPageVM()
    {
        Model = EmptyMaster;
        foreach(var m in _repo.GetAll())
            Models.Add(m);
    }
    #endregion
    #region Commands
    public CommandRelay DeleteCommand => field ??= new(Delete);
    public CommandRelay UpdateCommand => field ??= new(() => { });
    public CommandRelay AddCommand => field ??= new(Add);


    void Delete() => Models.Remove(Model);
    void Add() => Models.Add(Model);
    #endregion
}
