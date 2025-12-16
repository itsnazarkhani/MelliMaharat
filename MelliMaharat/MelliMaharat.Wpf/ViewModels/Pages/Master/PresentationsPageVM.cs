namespace MelliMaharat.Wpf.ViewModels.Pages.Master; 

public class PresentationsPageVM : BaseVM
{
    #region Constructors
    public PresentationsPageVM(Models.Master master)
    {
        Model = default!;
        foreach (var item in _repo.GetAll(master))
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
        }
    }
    public string MasterName 
    { 
        get
        {
            var result = Model.Master.User.PersonInformation.FirstName + " " + Model.Master.User.PersonInformation.LastName;
            return IsNullOrEmpty(result.Trim()) || IsNullOrWhiteSpace(result.Trim()) ? Empty : result;
        }
        set => Show("Master Name Should Not Be Changed!");
    }
    public string Name 
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
            nameof(Name) => "Lesson.Name",
            nameof(DayHold) => nameof(DayHold),
            nameof(StartTime) => nameof(StartTime),
            nameof(EndTime) => nameof(EndTime),
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
