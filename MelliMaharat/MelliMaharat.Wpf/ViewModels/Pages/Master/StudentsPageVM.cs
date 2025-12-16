namespace MelliMaharat.Wpf.ViewModels.Pages.Master;

public class StudentsPageVM : BaseVM
{
    #region Constructors
    public StudentsPageVM(Models.Master master)
    {
        Model = default!;
        foreach (var item in _repo.GetAll(master))
            Models.Add(item);
        ModelsView = CollectionViewSource.GetDefaultView(Models);
        SelectedOrderBy = nameof(FirstName);
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
        }
    }
    public string FirstName
    {
        get => Model.User.PersonInformation.FirstName;
        set
        {
            Model.User.PersonInformation.FirstName = value;
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string LastName
    {
        get => Model.User.PersonInformation.LastName;
        set
        {
            Model.User.PersonInformation.LastName = value;
            ValidateProperty(Model.User.PersonInformation);
        }
    }
    public string BirthDate
    {
        get => Model.User.PersonInformation.BirthDate == default ? Empty : Model.User.PersonInformation.BirthDate.ToString();
        set
        {
            Model.User.PersonInformation.BirthDate = DateOnly.Parse(value);
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
    public IReadOnlyList<string> OrderByOptions => [nameof(FirstName), nameof(LastName)];
    public ICollectionView ModelsView { get; }
    public string SelectedOrderBy
    {
        get => field;
        set
        {
            if (field == value)
                return;
            field = value;
            ApplySort(value);
        }
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
    void Add() => Models.Add(Model);
    void Update()
    {
        var (result, message) = _repo.Update(Model);
        if (result <= 0)
            Show(message, "Update Operation Failed!");
        else
        {
            var selected = Model;
            var index = Models.IndexOf(selected);

            if (index >= 0)
            {
                Models[index] = null!;
                Models[index] = selected;
            }
        }
    }
    #endregion
    #region Methods
    void ApplySort(string propertyName, [CallerMemberName] string caller = "")
    {
        var prop = propertyName switch
        {
            nameof(FirstName) => "User.PersonInformation.FirstNam",
            nameof(LastName) => "User.PersonInformation.LastName",
            _ => null
        };

        if (prop is null) return;

        using (ModelsView.DeferRefresh())
        {
            ModelsView.SortDescriptions.Clear();
            var sortDescription = new SortDescription(prop, ListSortDirection.Ascending);
            ModelsView.SortDescriptions.Add(sortDescription);
        }
        OnPropertyChanged(caller);
    }
    #endregion
}
