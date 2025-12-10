namespace MelliMaharat.Wpf.ViewModels.Pages.Student;

public class PresentationsPageVM : BaseVM
{
    #region Constructors
    public PresentationsPageVM(Models.Student student)
    {
        foreach (var item in _repo.GetAll(student))
            Models.Add(item);
        //if (Models.Count != 0)
        //    Model = Models.First();
        //else
        //    Model = new();
        Model = Models.Count != 0 ? Models.First() : new() { Lesson = new() { Name = "" } ,Master = new() { User = new() { PersonInformation = new() { FirstName = "", LastName = ""} } } };
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
            OnPropertyChanged();
            OnPropertyChanged(nameof(MasterName));
            OnPropertyChanged(nameof(LessonName));
            OnPropertyChanged(nameof(DayHold));
            OnPropertyChanged(nameof(StartTime));
            OnPropertyChanged(nameof(EndTime));
        }
    }
    public string MasterName 
    { 
        get
        {
            try
            {
                return Model.Master.User.PersonInformation.FirstName + " " + Model.Master.User.PersonInformation.LastName; 
            }
            catch
            {
                return Empty;
            }

        }
        set => throw new ArgumentException("This Property Should Not Be Touched!");
    }
    public string LessonName 
    {
        get
        {
            try
            {
                return Model.Lesson.Name;
            }
            catch
            {
                return Empty;
            }
        }
        set
        {
            Model.Lesson.Name = value;
            OnPropertyChanged();
            ValidateProperty(Model.Lesson);
        }
    }
    public string DayHold 
    {
        get
        {
            try
            {
                return Model.DayHold;
            }
            catch
            {
                return Empty;
            }
        }
        set
        {
            Model.DayHold = value;
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public string StartTime 
    {
        get
        {
            try
            {
                return Model.StartTime == new TimeOnly() ? Empty : Model.StartTime.ToString();
            }
            catch
            {
                return Empty;
            }
        }
        set
        {
            Model.StartTime = TimeOnly.Parse(value);
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public string EndTime 
    {
        get
        {
            try
            {
                return Model.EndTime == new TimeOnly() ? Empty : Model.EndTime.ToString();
            }                

            catch
            {
                return Empty;
            }
        }
        set
        {
            Model.EndTime = TimeOnly.Parse(value);
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    } 
    #endregion
}
