namespace MelliMaharat.Wpf.ViewModels.Pages.Manager;

public class PresentationsPageVM : BaseVM
{
    #region Constructors
    public PresentationsPageVM()
    {
        Model = EmptyPresentation;
        foreach (var item in _repo.GetAll())
            Models.Add(item);
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
            OnPropertyChanged();
            ValidateProperty(Model.Lesson);
        }
    }
    public string DayHold 
    { 
        get => Model.DayHold; 
        set
        {
            Model.DayHold = value;
            OnPropertyChanged();
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
                OnPropertyChanged();
                return;
            }
            OnPropertyChanged();
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
                OnPropertyChanged();
                return;
            }
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    #endregion
    #region Commands
    public CommandRelay DeleteCommand => field ??= new(() => { });
    public CommandRelay UpdateCommand => field ??= new(() => { });
    public CommandRelay AddCommand => field ??= new(() => { });
    #endregion
}
