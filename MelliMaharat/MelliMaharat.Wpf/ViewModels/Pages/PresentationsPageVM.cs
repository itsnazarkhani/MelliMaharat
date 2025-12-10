namespace MelliMaharat.Wpf.ViewModels.Pages;

public class PresentationsPageVM : BaseVM
{
    public PresentationsPageVM()
    {
        foreach (var item in _repo.GetAll())
            Models.Add(item);
        Model = Models.First();
    }
    readonly PresentationRepo _repo = new();

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
        get => Model.Master.User.PersonInformation.FirstName + " " + Model.Master.User.PersonInformation.LastName; 
        set
        {
            throw new ArgumentException("Master Name Should Not Be Changed");
        }
    }
    public string LessonName 
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
        get => Model.StartTime.ToString(); 
        set
        {
            Model.StartTime = TimeOnly.Parse(value);
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public string EndTime 
    { 
        get => Model.EndTime.ToString(); 
        set
        {
            Model.EndTime = TimeOnly.Parse(value);
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
}
