namespace MelliMaharat.Wpf.ViewModels.Pages.Manager;

public class SelectionsPageVM : BaseVM
{
    #region Constructors
    public SelectionsPageVM()
    {
        Model = EmptySelection;
        foreach (var item in _repo.GetAll())
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
            OnPropertyChanged(nameof(Student));
            OnPropertyChanged(nameof(Master));
            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Year));
        }
    }
    public string Student
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
    public string Master
    { 
        get 
        {
            var result = Model.Presentation.Master.User.PersonInformation.FirstName + " " + Model.Presentation.Master.User.PersonInformation.LastName; 
            return IsNullOrWhiteSpace(result.Trim()) || IsNullOrEmpty(result.Trim()) ? Empty : result;
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
                AddError(ex.Message);
                OnPropertyChanged();
                Model.Score = default;
                return;
            }
            OnPropertyChanged();
            ValidateProperty(Model);
        }
    }
    public string Name // Lesson Name
    { 
        get => Model.Presentation.Lesson.Name; 
        set
        {
            Model.Presentation.Lesson.Name = value;
            OnPropertyChanged();
            ValidateProperty(Model.Presentation.Lesson);
        }
    }
    public string Year // Education Year
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
    public CommandRelay DeleteCommand => field ??= new(() => { });
    public CommandRelay UpdateCommand => field ??= new(() => { });
    public CommandRelay AddCommand => field ??= new(() => { });
    #endregion
}
