namespace MelliMaharat.Wpf.ViewModels.Pages.Student;

public class ProfilePageVM : BaseVM
{
    #region Constructors
    public ProfilePageVM(Models.Student student)
    {
        Model = _repo.GetSingle(student);
        AvgGrade = _repo.GetAvgGrade(student);
    }
    #endregion
    #region Fields
    readonly StudentRepo _repo = new();
    #endregion
    #region Properties
    public Models.Student Model
    { 
        get => field; 
        set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(NationalCode));
            OnPropertyChanged(nameof(PhoneNumber));
            OnPropertyChanged(nameof(Username));
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
    public string BirthDate 
    {
        get => Model.User.PersonInformation.BirthDate.ToString();
        set
        {
            try
            {
                Model.User.PersonInformation.BirthDate = DateOnly.Parse(value);
            }
            catch (Exception ex)
            {
                Show(ex.Message);
                OnPropertyChanged();
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
    public string Id 
    { 
        get => Model.Id.ToString(); 
        set 
        {
            Show("This Field is Read-Only!");
            OnPropertyChanged();
        }
    }
    public decimal AvgGrade
    { 
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }
    #endregion
    #region Commands
    public CommandRelay ClearCommand => field ??= new(() => Model = EmptyStudent);
    public CommandRelay DeleteCommand => field ??= new(() => { });
    public CommandRelay UpdateCommand => field ??= new(() => { });
    public CommandRelay AddCommand => field ??= new(() => { });
    #endregion
}
