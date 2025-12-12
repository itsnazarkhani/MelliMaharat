namespace MelliMaharat.Wpf.ViewModels.Pages.Student;

public class SelectionsPageVM : BaseVM
{
    #region Constructors
    public SelectionsPageVM(Models.Student student)
    {
        Model = EmptySelection;
        foreach (var item in _repo.GetAll(student))
            Models.Add(item);
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
            field ??= EmptySelection;
            OnPropertyChanged();
            OnPropertyChanged(nameof(StudentFullName));
            OnPropertyChanged(nameof(MasterFullName));
            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(EducationYear));
            OnPropertyChanged(nameof(Unit));
        }
    }
    public string Unit 
    { 
        get => Model.Presentation.Lesson.Unit == default ? Empty : Model.Presentation.Lesson.Unit.ToString(); 
        set
        {
            Show("This Field Is Read-Only!");
            OnPropertyChanged();
        }
    }
    public string StudentFullName 
    {
        get
        {
            var result = Model.Student.User.PersonInformation.FirstName + " " + Model.Student.User.PersonInformation.LastName;
            return IsNullOrEmpty(result.Trim()) || IsNullOrWhiteSpace(result.Trim()) ? Empty : result;
        }
        set
        {
            Show("This Field Is Read-Only!");
            OnPropertyChanged();
        }
    }
    public string MasterFullName
    {
        get
        {
            var result = Model.Presentation.Master.User.PersonInformation.FirstName + " " + Model.Presentation.Master.User.PersonInformation.LastName;
            return IsNullOrEmpty(result.Trim()) || IsNullOrWhiteSpace(result.Trim()) ? Empty : result;
        }
        set
        {
            Show("This Field Is Read-Only!");
            OnPropertyChanged();
        }
    }
    public string Score 
    {
        get => Model.Score == default ? Empty : Model.Score.ToString();
        set
        {
            try
            {
                Model.Score = decimal.Parse(value);
            }
            catch (Exception ex)
            {
                Model.Score = default;
                AddError(ex.Message);
                OnPropertyChanged();
                return;
            }
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public string Name 
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
        get => Model.Term.Year == default ? Empty : Model.Term.Year.ToString();
        set
        {
            try
            {
                Model.Term.Year = int.Parse(value);
            }
            catch (Exception ex)
            {
                Model.Term.Year = default;
                AddError(ex.Message);
                OnPropertyChanged();
                return;
            }
            OnPropertyChanged();
            ValidateProperty(Model.Term);
        }
    }
    #endregion
    #region Commands
    public CommandRelay DeleteCommand => field ??= new(Delete);
    public CommandRelay UpdateCommand => field ??= new(() => { });
    public CommandRelay AddCommand => field ??= new(() => { });

    void Delete() => Models.Remove(Model);
    #endregion
}
