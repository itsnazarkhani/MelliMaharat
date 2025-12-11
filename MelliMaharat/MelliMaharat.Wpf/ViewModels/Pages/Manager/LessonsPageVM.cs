namespace MelliMaharat.Wpf.ViewModels.Pages.Manager;

public class LessonsPageVM : BaseVM
{
    #region Constructors
    public LessonsPageVM()
    {
        Model = EmptyLesson;
        foreach (var item in _repo.GetAll())
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
            OnPropertyChanged();
            ValidateProperty(Model);
            //DeleteCommand.NotifyCanExecuteChanged();
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
                OnPropertyChanged();
                AddError(x.Message);
                Model.Unit = default;
                return;
            }
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    #endregion
    #region Commands

    //CommandRelay? _clearCommand = null;
    public CommandRelay ClearCommand => field ??= new(() => Model = EmptyLesson);
    public CommandRelay DeleteCommand => field ??= new(() => 
    { 
        _repo.Remove(Model);

        Models.Clear();
        foreach (var item in _repo.GetAll())
            Models.Add(item);

        Model = EmptyLesson;
        Show("Lesson Deleted!");
    },
    HasError);
    public CommandRelay UpdateCommand => field ??= new( () => { });
    public CommandRelay AddComand => field ??= new(() => _repo.Add(Model), HasError);

    bool HasError() => !HasErrors;
    #endregion
}