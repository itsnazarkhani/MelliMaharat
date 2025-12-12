namespace MelliMaharat.Wpf.ViewModels.Pages.Master;

public class ProfilePageVM : BaseVM
{
    #region Constructors
    public ProfilePageVM(Models.Master master) => Model = _repo.GetSingle(master);
    #endregion
    #region Fields
    readonly MasterRepo _repo = new();
    #endregion
    #region Properties
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
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(NationalCode));
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(Graduation));
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(PhoneNumber));
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
        get => Model.User.PersonInformation.BirthDate == default ? Empty : Model.User.PersonInformation.BirthDate.ToString();
        set
        {
            try
            {
                Model.User.PersonInformation.BirthDate = DateOnly.Parse(value);
            }
            catch (Exception ex)
            {
                Model.User.PersonInformation.BirthDate = default;
                AddError(ex.Message);
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
    public string Id 
    { 
        get => Model.Id == default ? Empty : Model.Id.ToString(); 
        set
        {
            Show("This Property is Read Only!");
            OnPropertyChanged();
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
    #endregion
    #region Commands
    public CommandRelay DeleteCommand => field ??= new(Delete);
    public CommandRelay UpdateCommand => field ??= new(() => { });
    
    void Delete() => Model = default!;
    #endregion
}