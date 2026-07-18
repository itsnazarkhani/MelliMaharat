namespace MelliMaharat.Wpf.ViewModels.Pages.Manager;

public class PresentationsPageVM : BaseVM
{
    #region Constructors
    public PresentationsPageVM()
    {
        Model = default!;
        foreach (var item in _repo.GetAll())
            Models.Add(item);
        ModelsView = CollectionViewSource.GetDefaultView(Models);
        SelectedOrderBy = nameof(Name);
    }
    #endregion
    #region Fields
    readonly PresentationRepo _repo = new();
    #endregion
    #region Properties
    public ObservableCollection<Presentation> Models { get; set; } = [];
    public Presentation Model
    {
        get => field; 
        set
        {
            field = value;
            field ??= EmptyPresentation;
            ValidateAllProperties(this);
            OnPropertyChanged();
            OnPropertyChanged(nameof(MasterName));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(DayHold));
            OnPropertyChanged(nameof(StartTime));
            OnPropertyChanged(nameof(EndTime));
            OnPropertyChanged(nameof(NationalCode));
            OnPropertyChanged(nameof(Id));
        }
    }
    public string MasterName
    { 
        get
        {
            string fullName = Model.Master.User.PersonInformation.FirstName + " " + Model.Master.User.PersonInformation.LastName;
            return IsNullOrWhiteSpace(fullName.Trim()) || IsNullOrEmpty(fullName.Trim()) ? Empty : fullName;
        }
        set
        {
            Show("Master Name Should Not Be Changed");
            OnPropertyChanged();
        }
    }
    public string Name  // LessonName
    { 
        get => Model.Lesson.Name; 
        set
        {
            Model.Lesson.Name = value;
            ValidateProperty(Model.Lesson);
        }
    }
    public string DayHold 
    { 
        get => Model.DayHold; 
        set
        {
            Model.DayHold = value;
            ValidateProperty(Model);
        }
    }
    public string StartTime 
    { 
        get => Model.StartTime == default ? Empty : Model.StartTime.ToString(); 
        set
        {
            try
            {
                Model.StartTime = TimeOnly.Parse(value);
            }
            catch (Exception ex)
            {
                Model.StartTime = default;
                AddError(ex.Message);
                return;
            }
            ValidateProperty(Model);
        }
    }
    public string EndTime 
    { 
        get => Model.EndTime == default ? Empty : Model.EndTime.ToString(); 
        set
        {
            try
            {
                Model.EndTime = TimeOnly.Parse(value);
            }
            catch (Exception ex)
            {
                Model.EndTime = default;
                AddError(ex.Message);
                return;
            }
            ValidateProperty(Model);
        }
    }
    public string NationalCode // Master National-Code
    { 
        get => Model.Master.User.PersonInformation.NationalCode;
        //set => Show("This Field Is Read-Only!");
        set
        {
            Model.Master.User.PersonInformation.NationalCode = value;

            if (IsNullOrEmpty(value.Trim()) || IsNullOrWhiteSpace(value.Trim()))
            {
                AddError("Master National Code Is Required!");
                return;
            }

            ValidateProperty(Model.Master.User.PersonInformation);
        }
    }
    public string Id 
    { 
        get => Model.Id.ToString(); 
        set
        {
            try
            {
                Model.Id = Guid.Parse(value);
            }
            catch (Exception ex)
            {
                Show(ex.Message, "Try Not To Touch This!");
                return;
            }
            ValidateProperty(Model);
        }
    }
    public IReadOnlyList<string> OrderByOptions => [nameof(Name), nameof(DayHold), nameof(StartTime), nameof(EndTime)];
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
        
        if ( result <= 0)
            Show(message);
        else
            Models.Remove(Model);
    }
    void Add()
    {
        var (result, message) = _repo.Add(Model, NationalCode, Name);

        if (result <= 0)
        {
            Show(message);
            return;
        }
            
        Models.Add(Model);
        OnPropertyChanged(nameof(Model));
        OnPropertyChanged(nameof(Id));
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
        OnPropertyChanged(nameof(Model));
        OnPropertyChanged(nameof(Id));
    }
    #endregion
    #region Methods
    void ApplySort(string propertyName, [CallerMemberName] string caller = "")
    {
        var prop = propertyName switch
        {
            nameof(Name) => "Lesson.Name",
            nameof(DayHold) => nameof(DayHold),
            nameof(StartTime) => nameof(StartTime),
            nameof(EndTime) => nameof(EndTime),
            _ => null
        };

        if (prop == null) return;

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
