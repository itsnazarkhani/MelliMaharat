namespace MelliMaharat.Wpf.ViewModels.Pages.Student;

public class SelectionsPageVM : BaseVM
{
    #region Constructors
    public SelectionsPageVM(Models.Student student)
    {
        foreach (var item in _repo.GetAll(student))
            Models.Add(item);
        Model = Models.Count != 0 ? Models.First() : new() {Term = new() { Year = 0 } ,Student = new() { User = new() { PersonInformation = new() {FirstName = "", LastName = "" } } }, Presentation = new() { Lesson = new() { Name = "" } , Master = new() { User = new() { PersonInformation = new() { FirstName = "", LastName = "" } } } } };
    }
    #endregion
    #region Fields
    readonly SelectionRepo _repo = new();
    #endregion
    #region Properties
    public ObservableCollection<Selection> Models { get; set; } = [];
    public Selection Model
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(StudentFullName));
            OnPropertyChanged(nameof(MasterFullName));
            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(EducationYear));
        }
    }
    public string StudentFullName 
    { 
        get
        {
            try
            {
                return Model.Student.User.PersonInformation.FirstName + " " + Model.Student.User.PersonInformation.LastName;
            }
            catch
            {
                return Empty;
            }
        }
        set => throw new ArgumentException("This Field Is Read-Only!");
    }
    public string MasterFullName 
    {
        get
        {
            try
            {
                return Model.Presentation.Master.User.PersonInformation.FirstName + " " + Model.Presentation.Master.User.PersonInformation.LastName;
            }
            catch
            {
                return Empty;
            }
        }
        set => throw new ArgumentException("This Field Is Read-Only!");
    }
    public string Score 
    {
        get 
        {
            try
            {
                return Model.Score == 0 ? Empty : Model.Score.ToString();
            }
            catch
            {
                return Empty;
            }   
        }  
        set
        {
            Model.Score = decimal.Parse(value);
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public string Name 
    {
        get
        {
            try
            {
                return Model.Presentation.Lesson.Name;
            }
            catch 
            {
                return Empty;
            }   
        }
        set
        {
            Model.Presentation.Lesson.Name = value;
            OnPropertyChanged();
            ValidateProperty(Model.Presentation.Lesson);
        }
    }
    public string EducationYear 
    {
        get
        {
            try
            {
                return Model.Term.Year == 0 ? Empty :  Model.Term.Year.ToString();
            }
            catch
            {
                return Empty;
            }
        }
        set
        {
            Model.Term.Year = int.Parse(value);
            OnPropertyChanged();
            ValidateProperty(Model.Term);
        }
    }
    #endregion
}
