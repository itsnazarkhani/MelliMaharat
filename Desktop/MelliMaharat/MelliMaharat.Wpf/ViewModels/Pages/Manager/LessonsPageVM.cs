namespace MelliMaharat.Wpf.ViewModels.Pages.Manager;

public class LessonsPageVM : BaseVM
{
    #region Constructors
    public LessonsPageVM()
    {
        Model = default!;
        foreach (var item in _repo.GetAll())
            Models.Add(item);
        ModelsView = CollectionViewSource.GetDefaultView(Models);
        SelectedOrderBy = nameof(Name);
    }
    #endregion
    #region Fields
    readonly LessonRepo _repo = new();
    #endregion
    #region Properties
    public ObservableCollection<Lesson> Models { get; set; } = [];
    public Lesson Model
    { 
        get => field; 
        set
        {
            field = value;
            field ??= EmptyLesson;
            ValidateAllProperties(this);
            OnPropertyChanged();
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Unit));
            DeleteCommand.NotifyCanExecuteChanged();
        }
    }
    public string Name
    { 
        get =>  Model.Name; 
        set
        {
            Model.Name = value;
            ValidateProperty(Model);
        }
    }
    public string Unit
    { 
        get => Model.Unit == default ? Empty : Model.Unit.ToString();
        set
        {
            try
            {
                Model.Unit = int.Parse(value);
            }
            catch (Exception x)
            {
                Model.Unit = default;
                AddError(x.Message);
                return;
            }
            ValidateProperty(Model);
        }
    }
    public IReadOnlyList<string> OrderByOptions => [nameof(Name), nameof(Unit)];
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
    public CommandRelay AddComand => field ??= new(Add);

    bool HasError() => !HasErrors;
    void Delete()
    {
        var (result, message) = _repo.Remove(Model);

        if ( result <= 0)    
            Show(message);
        else
            Models.Remove(Model);
    }
    void Update()
    {
        var (result, message) = _repo.Update(Model);

        if (result <= 0)
            Show(message);
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
    void Add()
    {
        var (result, message) = _repo.Add(Model);
        
        if ( result <= 0)
            Show(message);
        else
            Models.Add(Model);
    }

    #endregion
    #region Methods
    void ApplySort(string propertyName, [CallerMemberName] string caller = "")
    {
        string? prop = propertyName switch
        {
            nameof(Name) => "Name",
            nameof(Unit) => "Unit",
            _ => null
        };
        if (prop == null)
            return;

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