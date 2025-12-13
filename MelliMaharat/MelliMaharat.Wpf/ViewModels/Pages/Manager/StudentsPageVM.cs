namespace MelliMaharat.Wpf.ViewModels.Pages.Manager;

public class StudentsPageVM : BaseVM
{
    #region Constructors
    public StudentsPageVM()
    {
        Model = EmptyStudent;
        foreach(var item in _repo.GetAll())
            Models.Add(item);
    }
    #endregion
    #region Fields
    readonly StudentRepo _repo = new();
    #endregion
    #region Properties
    public ObservableCollection<Models.Student> Models { get; set; } = [];
    public Models.Student Model
    {
        get => field; 
        set
        {
            field = value;
            field ??= EmptyStudent;
            ValidateAllProperties(this);
            OnPropertyChanged();
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(NationalCode));
            OnPropertyChanged(nameof(PhoneNumber));
            OnPropertyChanged(nameof(Email));
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
                return;
            }
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string NationalCode 
    { 
        get => Model.User.PersonInformation.NationalCode; 
        set
        {
            Model.User.PersonInformation.NationalCode = value;
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string PhoneNumber 
    { 
        get => Model.User.PersonInformation.PhoneNumber; 
        set
        {
            Model.User.PersonInformation.PhoneNumber = value;
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string Email 
    { 
        get => Model.User.Email; 
        set
        {
            Model.User.Email = value;
            ValidateProperty(Model.User);
        }
    }
    public string Username 
    { 
        get => Model.User.Username; 
        set
        {
            Model.User.Username = value;
            ValidateProperty(Model.User);
        }
    }
    public string Id 
    { 
        get => Model.Id == default ? Empty : Model.Id.ToString();
        set => Show("Id Should Not Be Able To Set By User!");
    }
    #endregion
    #region Commands
    public CommandRelay DeleteCommand => field ??= new(Delete);
    public CommandRelay UpdateCommand => field ??= new(Update);
    public CommandRelay AddCommand => field ??= new(Add);


    void Delete()
    {
        var (result, message) = _repo.Remove(Model);
        if (result <= 0)
            Show(message, "Delete Operation Failed!");
        else
            Models.Remove(Model);
    }
    void Add()
    {
        var (result, message) = _repo.Add(Model);
        
        if (result <= 0)
            Show(message, "Add Operation Failed!");
        else
            Models.Add(Model);
    }
    void Update()
    {
        var (result, message) = _repo.Update(Model);
        
        if (result <= 0)
        {
            Show(message);
            return;
        }

        var selected = Model;
        var index = Models.IndexOf(selected);
        
        if (index >= 0)
        {
            Models[index] = null!;
            Models[index] = selected;
        }
    }
    #endregion
}
