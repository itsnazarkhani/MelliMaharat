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
}