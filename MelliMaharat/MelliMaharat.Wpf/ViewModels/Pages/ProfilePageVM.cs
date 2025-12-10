namespace MelliMaharat.Wpf.ViewModels.Pages;

public class ProfilePageVM : BaseVM
{
    public ProfilePageVM(User user) => Model = user;

    #region Properties
    public User Model
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
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(Id));
        }
    }
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
