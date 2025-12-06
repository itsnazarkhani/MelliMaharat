namespace MelliMaharat.Wpf.ViewModels.Pages;

class ProfilePageVM : BaseVM<User>
{
    #region Constructors
    public ProfilePageVM() : base() { }
    public ProfilePageVM(User user) : base(user) { }
    #endregion

    #region Properties
    public string? FirstName
    {
        get => Model.PersonInformation.FirstName;
        set 
        {
            Model.PersonInformation.FirstName = value;
            OnPropertyChanged();
            ValidateProperty(Model.PersonInformation);
        }
    }

    public string? LastName
    {
        get => Model.PersonInformation.LastName;
        set
        {
            Model.PersonInformation.LastName = value;
            OnPropertyChanged();
            ValidateProperty(Model.PersonInformation);
        }
    }
    public string? Password
    {
        get => Model.Password;
        set
        {
            Model.Password = value;
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public string? Email
    {
        get => Model.Email;
        set
        {
            Model.Email = value;
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public DateOnly BirthDate
    {
        get => Model.PersonInformation.BirthDate;
        set
        {
            // should convert string to value.
            //Model.PersonInformation.BirthDate = value;
            OnPropertyChanged();
            ValidateProperty(Model.PersonInformation);
        }
    }
    public string? NationalCode
    {
        get => Model.PersonInformation.NationalCode;
        set
        {
            Model.PersonInformation.NationalCode = value;
            OnPropertyChanged();
            ValidateProperty(Model.PersonInformation);
        }
    }
    public string? PhoneNumber
    { 
        get => Model.PersonInformation.PhoneNumber; 
        set
        {
            Model.PersonInformation.PhoneNumber = value;
            OnPropertyChanged();
            ValidateProperty(Model.PersonInformation);
        }
    }
    public string? Username 
    {
        get => Model.Username; 
        set
        {
            Model.Username = value;
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public bool? IsAdmin
    { 
        get => Model.Role == UserRoles.Admin; 
        set
        {
            // should create converter.
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public Guid Id 
    { 
        get => Model.Id; 
        set
        {
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    #endregion
}
