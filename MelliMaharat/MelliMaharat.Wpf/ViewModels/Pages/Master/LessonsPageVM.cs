namespace MelliMaharat.Wpf.ViewModels.Pages.Master;

public class LessonPageVM : BaseVM
{
    #region Constructors
    public LessonPageVM(Models.Master master)
    {
        foreach (var item in _repo.GetAll(master))
            Models.Add(item);
        Model = Models.First();
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
        get => Model.Unit.ToString(); 
        set
        {
            Model.Unit = int.Parse(value);
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    #endregion
}
