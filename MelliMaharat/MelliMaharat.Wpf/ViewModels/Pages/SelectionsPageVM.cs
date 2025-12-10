namespace MelliMaharat.Wpf.ViewModels.Pages;

public class SelectionsPageVM : BaseVM
{
    public SelectionsPageVM()
    {
        foreach (var item in _repo.GetAll())
            Models.Add(item);
        Model = Models.First();
    }

    readonly SelectionRepo _repo = new();
    public ObservableCollection<Selection> Models { get; set; } = [];
    public Selection Model
    { 
        get => field; 
        set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(StudentFirstName));
            OnPropertyChanged(nameof(StudentLastName));
            OnPropertyChanged(nameof(MasterFirstName));
            OnPropertyChanged(nameof(MasterLastName));
            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(LessonName));
            OnPropertyChanged(nameof(EducationYear));
        }
    }
    public string StudentFirstName
    { 
        get => Model.Student.User.PersonInformation.FirstName; 
        set
        {
            Model.Student.User.PersonInformation.FirstName = value;
            OnPropertyChanged();
            ValidateProperty(Model.Student.User.PersonInformation);
        }
    }
    public string StudentLastName 
    { 
        get => Model.Student.User.PersonInformation.LastName; 
        set
        {
            Model.Student.User.PersonInformation.LastName = value;
            OnPropertyChanged();
            ValidateProperty(Model.Student.User.PersonInformation);
        }
    }
    public string MasterFirstName 
    { 
        get => Model.Presentation.Master.User.PersonInformation.FirstName; 
        set
        {
            Model.Presentation.Master.User.PersonInformation.FirstName = value;
            OnPropertyChanged();
            ValidateProperty(Model.Presentation.Master.User.PersonInformation);
        }
    }
    public string MasterLastName 
    { 
        get => Model.Presentation.Master.User.PersonInformation.LastName;
        set
        {
            Model.Presentation.Master.User.PersonInformation.LastName = value;
            OnPropertyChanged();
            ValidateProperty(Model.Presentation.Master.User.PersonInformation);
        }
    }
    public string Score 
    { 
        get => Model.Score.ToString(); 
        set
        {
            Model.Score = decimal.Parse(value);
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public string LessonName 
    { 
        get => Model.Presentation.Lesson.Name; 
        set
        {
            Model.Presentation.Lesson.Name = value;
            OnPropertyChanged();
            ValidateProperty(Model.Presentation.Lesson);
        }
    }
    public string EducationYear 
    { 
        get => Model.Term.Year.ToString(); 
        set
        {
            Model.Term.Year = int.Parse(value);
            OnPropertyChanged();
            ValidateProperty(Model.Term);
        }
    }
}
