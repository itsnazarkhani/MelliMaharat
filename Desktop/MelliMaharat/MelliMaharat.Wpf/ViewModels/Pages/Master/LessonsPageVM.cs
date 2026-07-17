namespace MelliMaharat.Wpf.ViewModels.Pages.Master;

public class LessonPageVM : BaseVM
{
    #region Constructors
    public LessonPageVM(Models.Master master)
    {
        Model = default!;
        foreach (var item in _repo.GetAll(master))
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
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Unit));
        }
    }
    public string Name 
    { 
        get => Model.Name; 
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
            catch (Exception ex)
            {
                Model.Unit = default;
                AddError(ex.Message);
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
            nameof(Name) => nameof(Name),
            nameof(Unit) => nameof(Unit),
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
