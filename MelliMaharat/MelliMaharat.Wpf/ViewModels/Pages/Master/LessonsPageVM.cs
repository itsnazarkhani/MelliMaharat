namespace MelliMaharat.Wpf.ViewModels.Pages.Master;

public class LessonPageVM : BaseVM
{
    #region Constructors
    public LessonPageVM(Models.Master master)
    {
        Model = EmptyLesson;
        foreach (var item in _repo.GetAll(master))
            Models.Add(item);
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
            OnPropertyChanged();
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
            OnPropertyChanged();
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
                OnPropertyChanged();
            }
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    #endregion
    #region Commands
    public CommandRelay DeleteCommand => field ??= new(Delete);
    public CommandRelay UpdateCommand => field ??= new(Update);
    public CommandRelay AddCommand => field ??= new(Add);

    void Delete() => Models.Remove(Model);
    void Add() => Models.Add(Model);
    void Update()
    {
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
